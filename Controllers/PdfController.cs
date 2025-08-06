using System.Globalization;
using AutoMapper;
using MealPlannerApi.Data;
using MealPlannerApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MealPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IMapper _mapper;
        

        public PdfController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("generatePdf")]
        public async Task<IActionResult> GeneratePdf(GeneratePdfRequestDto dto)
        {
            var ingredients = _unitOfWork.MealPlanItemRepository
                .Search(mpi => mpi.Date >= dto.startDate && mpi.Date <= dto.endDate)
                .Select(mpi => _mapper.Map<MealPlanItemDto>(mpi))
                .SelectMany(mpi => mpi.Recipe.Ingredients)
                .GroupBy(x => x.Ingredient.Id)
                .Select(item => new
                {
                    Id = item.Key,
                    Quantity = item.Sum(i => i.Quantity),
                    Name = item.First().Ingredient.Name,
                    Unit = item.First().Ingredient.UnitMeasure.Abbreviation,
                });

            var culture = new CultureInfo("es-MX");
            var format = "d 'de' MMMM";
            
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text($"Lista de compras {dto.startDate.ToString(format, culture)} al  {dto.endDate.ToString(format, culture)}")
                        .SemiBold().FontSize(36).FontColor(Colors.Black);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Table(t =>
                        {
                            t.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            
                            t.Header(header =>
                            {
                                header.Cell().BorderBottom(2).Padding(8).Text("Ingrediente").AlignCenter();
                                header.Cell().BorderBottom(2).Padding(8).Text("Cantidad").AlignCenter();
                                header.Cell().BorderBottom(2).Padding(8).Text("Unidad").AlignCenter();
                            });

                            foreach (var item in ingredients)
                            {
                                t.Cell().BorderBottom(1).Padding(4).Text(item.Name).AlignCenter();
                                t.Cell().BorderBottom(1).Padding(4).Text(item.Quantity.ToString()).AlignCenter();
                                t.Cell().BorderBottom(1).Padding(4).Text(item.Unit).AlignCenter();
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text("Desarrollador por AngelAlmanza");
                });
            }).GeneratePdf();
            return File(document, "application/pdf", "MealPlannerApi.pdf");
        }
    }
}