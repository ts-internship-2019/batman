using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.DTOs;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iWasHere.Domain.Model;
using System.Collections.Generic;
using System.IO;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System;

namespace iWasHere.Web.Controllers
{

    public class AttractionController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly AttractionService _attractionService;

        public AttractionController(AttractionService attractionService, IHostingEnvironment hostingEnvironment)
        {
            _attractionService = attractionService;
            _hostingEnvironment = hostingEnvironment;

        }

        public ActionResult Attraction(int attrId)
        {
            AttractionModel attr = new AttractionModel();
            attr = _attractionService.GetAttractionLocation(attrId);
            return View(attr);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAttractions([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_attractionService.GetAttractionListModels().ToDataSourceResult(request));
        }

        public ActionResult ExportDocument(int AttractionId)
        {
            OpenSettings os = new OpenSettings();
            os.AutoSave = false;
            AttractionModel Attraction = new AttractionModel();
            Attraction = _attractionService.GetAttractionToExport(AttractionId);
            var path = Path.Combine(_hostingEnvironment.WebRootPath + "/Attraction_Download/Attraction.docx");
            string resultPath = Path.Combine(_hostingEnvironment.WebRootPath + "/Attraction_Download/Attraction_Saved.docx");
            byte[] byteArray = System.IO.File.ReadAllBytes(path);
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(stream, true))
                {

                    Body body = wordDoc.MainDocumentPart.Document.Body;
                    var paragraphs = body.Elements<Paragraph>();

                    int? MaxRating = 0;
                    int? MinRating = 10;
                    int? MaxRating2 = 0;
                    int? MinRating2 = 10;
                    string CommentMax1 = "";
                    string CommentMax2 = "";
                    string CommentMin1 = "";
                    string CommentMin2 = "";
                    int j1 = -1; //pozitia min 1
                    int j2 = -1; //pozitia max 1
                    if (Attraction.Comment.Count > 0)
                    {
                        for (int i = 0; i < Attraction.Comment.Count; i++)
                        {
                            if (Attraction.Comment[i].Rating <= MinRating)
                            {
                                MinRating = Attraction.Comment[i].Rating;
                                CommentMin1 = Attraction.Comment[i].CommentText;
                                j1 = i;
                            }
                            if (Attraction.Comment[i].Rating >= MaxRating)
                            {
                                MaxRating = Attraction.Comment[i].Rating;
                                CommentMax1 = Attraction.Comment[i].CommentText;
                                j2 = i;
                            }
                        }
                        for (int i = 0; i < Attraction.Comment.Count; i++)
                        {
                            if (Attraction.Comment[i].Rating == MinRating && i != j1)
                            {
                                MinRating2 = Attraction.Comment[i].Rating;
                                CommentMin2 = Attraction.Comment[i].CommentText;
                            }
                            if (Attraction.Comment[i].Rating == MaxRating && i != j2)
                            {
                                MaxRating2 = Attraction.Comment[i].Rating;
                                CommentMax2 = Attraction.Comment[i].CommentText;
                            }
                        }
                    }

                    foreach (Paragraph paragraph in paragraphs)
                    {
                        foreach (Run run in paragraph.Elements<Run>())
                        {
                            foreach (Text text in run.Elements<Text>())
                            {
                                if (text.Text == "Attraction_Name")
                                {
                                    text.Text = Attraction.AttractionName;
                                }
                                if (text.Text == "XY")
                                {
                                    int? rating = 0;
                                    string stars = "";
                                    if (Attraction.Comment.Count > 0)
                                    {
                                        for (int i = 0; i < Attraction.Comment.Count; i++)
                                        {
                                            rating = rating + Attraction.Comment[i].Rating;
                                        }
                                        rating = rating / Attraction.Comment.Count;
                                        for (int i = 0; i < rating; i++)
                                            stars = stars + "* ";
                                        text.Text = stars;
                                    }
                                    else text.Text = "";
                                }
                                if (text.Text == "Country")
                                {
                                    text.Text = Attraction.CountryName;
                                }
                                if (text.Text == "County")
                                {
                                    text.Text = Attraction.CountyName;
                                }
                                if (text.Text == " City")
                                {
                                    text.Text = Attraction.CityName;
                                }


                                if (Attraction.Comment.Count > 0)
                                { 
                                    if (text.Text == "Pro1")
                                    {
                                        text.Text = CommentMax1;
                                    }
                                    if (text.Text == "Pro2")
                                    {
                                        text.Text = CommentMax2;
                                    }
                                    if (text.Text == "Rau")
                                    {
                                        text.Text = CommentMin1;
                                    }
                                    if (text.Text == "zyx")
                                    {
                                        text.Text = CommentMin2;
                                    }
                                }
                                else
                                {
                                    if (text.Text == "Pareri Pozitive ")
                                    {
                                        text.Text = "Nu exista comentarii.";
                                    }
                                    if (text.Text == "Pareri Negative ")
                                    {
                                        text.Text = "";
                                    }
                                    if (text.Text == "Pro1")
                                    {
                                        text.Text = "";
                                    }
                                    if (text.Text == "Pro2")
                                    {
                                        text.Text = "";
                                    }
                                    if (text.Text == "Rau")
                                    {
                                        text.Text = "";
                                    }
                                    if (text.Text == "zyx")
                                    {
                                        text.Text = "";
                                    }
                                }
                            }
                        }
                    }

                    if (Attraction.Photo.Count > 0)
                    {
                        string FirstPhoto = Attraction.Photo[0].PhotoName;
                        ImagePart imagePart = wordDoc.MainDocumentPart.AddImagePart(ImagePartType.Jpeg);

                        using (FileStream streamPhoto = new FileStream(_hostingEnvironment.WebRootPath + "/images/" + FirstPhoto, FileMode.Open))
                        {
                            imagePart.FeedData(streamPhoto);
                        }
                        AddImageToBody(AttractionId, wordDoc, wordDoc.MainDocumentPart.GetIdOfPart(imagePart));
                    }
                }
                // Save the file with the new name
                System.IO.File.WriteAllBytes(resultPath, stream.ToArray());
            }

            string FileName = "Attraction_" + Attraction.AttractionId + ".docx";

            
            byte[] bytes = System.IO.File.ReadAllBytes(resultPath);
            FileContentResult file = File(bytes, MediaTypeNames.Application.Octet, FileName);

            return file;
        }
        public ActionResult AddOrEditAttraction(int attrId)
        {


            if (attrId != 0)
            {
                AttractionModel attr = new AttractionModel();
                attr = _attractionService.GetAttractionLocation(attrId);
                ViewData["Images"] = _attractionService.GetPhotosByAttractionId(attrId);
                return View(attr);
            }
            return View(new AttractionModel());
        }
        [HttpPost]
        public IActionResult SaveAttraction(AttractionModel attraction, List<IFormFile> files)
        {
            _attractionService.SaveAttraction(attraction, out string errorMessage, out int id);
            SubmitPhoto(id, files);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                TempData["message"] = errorMessage;
                return RedirectToAction("AddEditNewLandmark", new { attrId = attraction.AttractionId });
            }
            return RedirectToAction("Attraction", new { attrId = attraction.AttractionId });

        }
        private IEnumerable<string> GetFileInfo(IEnumerable<IFormFile> files, int attractionId)
        {
            int status = 0;
            List<string> fileInfo = new List<string>();

            foreach (var file in files)
            {
                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                var filePath = Path.GetFullPath(fileContent.FileName.ToString().Trim('"'));

                status = _attractionService.AddPhoto(attractionId, fileName);
                fileInfo.Add(string.Format("{0} ({1} bytes)", fileName, file.Length, filePath));
            }

            return fileInfo;
        }
        public ActionResult SubmitPhoto(int attractionId, IEnumerable<IFormFile> files)
        {
            int status = 0;
            //attractionId = 14;
            IEnumerable<string> fileInfo = new List<string>();

            if (files != null)
            {
                fileInfo = GetFileInfo(files, attractionId);
            }
            return Json(status);
        }




        public void AddImageToBody(int Id, WordprocessingDocument wordDoc, string relationshipId)
        {
            AttractionModel Attraction = new AttractionModel();
            Attraction = _attractionService.GetAttractionToExport(Id);
            string FirstPhoto = Attraction.Photo[0].PhotoName;
            int iWidth = 0;
            int iHeight = 0;
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(_hostingEnvironment.WebRootPath + "/images/" + FirstPhoto))
            {
                iWidth = bmp.Width;
                iHeight = bmp.Height;
            }
            iWidth = (int)Math.Round((decimal)iWidth * 3380);
            iHeight = (int)Math.Round((decimal)iHeight * 3380);

            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = iWidth, Cy = iHeight },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            wordDoc.MainDocumentPart.Document.Body.AppendChild(
             new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(element))
             {
                 ParagraphProperties = new DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties()
                 {
                     Justification = new DocumentFormat.OpenXml.Wordprocessing.Justification()
                     {
                         Val = DocumentFormat.OpenXml.Wordprocessing.JustificationValues.Center
                     }
                 }
             });
        }
       
        public ActionResult AttractionsCountry(int countryId)
        {
            List<AttractionListModel> attrList = new List<AttractionListModel>();
            attrList = _attractionService.GetAttractionsFromCountry(countryId);
            return View(attrList);

        }
    }
}

