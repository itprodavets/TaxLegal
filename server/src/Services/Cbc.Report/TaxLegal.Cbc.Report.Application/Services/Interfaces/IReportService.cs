using System.Collections.Generic;
using System.IO;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Schemas;

namespace TaxLegal.Cbc.Report.Application.Services.Interfaces
{
    public interface IReportService
    {
        ReportData? Parse(Stream stream);
        string Generate(ReportData data);
        IReadOnlyCollection<ValidationMessage> Validate(SupportedSchema schema, Stream file);
    }
}