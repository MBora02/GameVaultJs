using GameVaultJs.Data;
using GameVaultJs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GameVaultJs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GenreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GenreList()
        {
            var genre = _context.Genres.ToList();
            return new JsonResult(genre);
        }


        [HttpPost]
        public JsonResult AddGenre(Genre genre)
        {
            var g = new Genre()
            {
                Name = genre.Name,
                Description = genre.Description,
                GameCount=genre.GameCount
            };
            _context.Genres.Add(g);
            _context.SaveChanges();
            return new JsonResult("Data saved");
        }


        [HttpGet]
        public JsonResult Edit(int id)
        {
            var data = _context.Genres.Where(m => m.Id == id).SingleOrDefault();
            return new JsonResult(data);
        }

        [HttpPost]
        public JsonResult Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return new JsonResult("Data updated");
        }

        public JsonResult Delete(int id)
        {
            var data = _context.Genres.Where(m => m.Id == id).SingleOrDefault();
            _context.Genres.Remove(data);
            _context.SaveChanges();
            return new JsonResult("Data deleted");
        }

        public IActionResult ExportToPdf()
        {
            // 1. Veri tabanından güncel listeyi çekin
            var products = _context.Genres.ToList();

            // 2. QuestPDF ile PDF dökümanını tasarlayın
            var pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    // Üst Bilgi (Header)
                    page.Header()
                        .Text("Pet Listesi Raporu")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    // İçerik (Tablo Oluşturma)
                    page.Content()
                        .PaddingTop(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            // Sütun genişliklerini tanımlayın
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(50);  // ID sütunu genişliği
                                columns.RelativeColumn();    // Genre adı sütunu (esnek)
                                columns.ConstantColumn(100); // açıklama sütunu genişliği
                                columns.RelativeColumn(); // oyun sayısı sütunu genişliği
                            });

                            // Tablo Başlıkları (Header Row)
                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("ID").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Genre Adı").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Açıklama").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Oyun Sayısı").Bold();
                            });

                            // Veri Satırları (Döngü ile verileri basıyoruz)
                            foreach (var item in products)
                            {
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Id.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Name);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Description);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.GameCount.ToString());
                            }
                        });

                    // Alt Bilgi (Footer)
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sayfa ");
                            x.CurrentPageNumber();
                        });
                });
            });

            // 3. PDF'i byte dizisine çevirip tarayıcıya indirtme
            var pdfBytes = pdfDocument.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Genre_Listesi_{DateTime.Now:yyyyMMdd}.pdf");
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Backend softito");

            // 2. Veri tabanından güncel listenizi çekin
            var products = _context.Genres.ToList();

            // 3. Bellekte (Memory) boş bir Excel dosyası oluşturun
            using (var package = new ExcelPackage())
            {
                // Excel içinde "Genre Listesi" adında bir sayfa aç
                var worksheet = package.Workbook.Worksheets.Add("Genre Listesi");

                // 4. Tablo Başlıklarını Yazın (1. Satır)
                worksheet.Cells[1, 1].Value = "Genre ID";
                worksheet.Cells[1, 2].Value = "Genre Adı";
                worksheet.Cells[1, 3].Value = "Açıklama";
                worksheet.Cells[1, 4].Value = "Oyun Sayısı";

                // 5. Başlık Satırını Şıklaştırın (Arka plan rengi, kalın yazı vb.)
                using (var range = worksheet.Cells[1, 1, 1, 4]) // 1. satır, 1'den 4. sütuna kadar seç
                {
                    range.Style.Font.Bold = true; // Yazıyı kalın yap
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(41, 128, 185)); // Mavi arka plan
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White); // Beyaz yazı rengi
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Ortala
                }

                // 6. Verileri Döngü ile Excel Satırlarına Basın
                int rowNumber = 2; // Veriler 2. satırdan başlayacak
                foreach (var item in products)
                {
                    worksheet.Cells[rowNumber, 1].Value = item.Id;
                    worksheet.Cells[rowNumber, 2].Value = item.Name;
                    worksheet.Cells[rowNumber, 3].Value = item.Description;
                    worksheet.Cells[rowNumber, 4].Value = item.GameCount;



                    rowNumber++;
                }



                //7.Sütun genişliklerini içeriğe göre otomatik ayarla

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // 8. Excel dosyasını byte dizisine çevirip tarayıcıya fırlat
                var fileBytes = package.GetAsByteArray();
                string fileName = $"Genre_Listesi_{DateTime.Now:yyyyMMdd}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);







            }
        }
    }
}
