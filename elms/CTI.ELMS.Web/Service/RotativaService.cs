using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace CTI.ELMS.Web.Service
{
    public class RotativaService<TModel>
    {
        public RotativaService(TModel model, string reportTemplatePath, string fileName, string staticFolder, string staticFolderPath, string subFolderPath, Orientation orientation = Orientation.Portrait,
            Size size = Size.A4)
        {
            _model = model;
            _reportTemplatePath = reportTemplatePath;
            _fileName = fileName;
            _staticFolder = staticFolder;
            _staticFolderPath = staticFolderPath;
            _subFolderPath = subFolderPath;
            _orientation = orientation;
            _size = size;
        }
        private readonly TModel _model;
        private readonly string _reportTemplatePath;
        private readonly string _fileName;
        private readonly string _staticFolder;
        private readonly string _staticFolderPath;
        private readonly string _subFolderPath;
        private readonly Orientation _orientation;
        private readonly Size _size;
        public async Task<RotativaDocumentModel> GeneratePDFAsync(Microsoft.AspNetCore.Mvc.ActionContext pageContext)
        {
            RotativaService<TModel>.CreateFolderIfNotExists(_staticFolderPath + "\\" + _subFolderPath);
            var rotativaDocument = new RotativaDocumentModel(_fileName, _staticFolderPath, _subFolderPath, _staticFolder);
            if (File.Exists(rotativaDocument.CompleteFilePath)) { File.Delete(rotativaDocument.CompleteFilePath); }
            var document = new ViewAsPdf($"..\\{_reportTemplatePath}", _model)
            {
                PageOrientation = _orientation,
                PageSize = _size,
                PageMargins = new Margins(5, 0, 10, 0)
            };
            var byteArray = await document.BuildFile(pageContext);
            var fileStream = new FileStream(rotativaDocument.CompleteFilePath, FileMode.Create, FileAccess.Write);
            fileStream.Write(byteArray, 0, byteArray.Length);
            fileStream.Close();
            return rotativaDocument;
        }
        private static void CreateFolderIfNotExists(string folderPath)
        {
            bool folderPathExists = Directory.Exists(folderPath);
            if (!folderPathExists)
                Directory.CreateDirectory(folderPath);
        }
    }
    public class RotativaDocumentModel
    {
        public RotativaDocumentModel()
        {
        }
        public RotativaDocumentModel(string fileName, string staticFolderPath, string subFolderPath, string staticFolder)
        {
            this.FileName = fileName;
            this.CompleteFilePath = staticFolderPath + "\\" + subFolderPath + "\\" + this.FileName;
            this.FileUrl = "\\" + staticFolder + "\\" + subFolderPath + "\\" + this.FileName;
        }
        public string FileName { get; set; } = "";
        public string CompleteFilePath { get; private set; } = "";
        public string FileUrl { get; private set; } = "";
    }
}
