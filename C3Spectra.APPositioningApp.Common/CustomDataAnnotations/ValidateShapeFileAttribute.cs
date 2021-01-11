using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace C3Spectra.APPositioningApp.Common.CustomDataAnnotations
{
    public class ValidateShapeFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int cprj = 0, cqpj = 0, csbn = 0, csbx = 0, cshp = 0, cdbf = 0, cshx = 0;
            string extensions = Helper.GetAppsettingValue("ShapeZipExtensions");
            string[] arrExtensions = extensions.Split(',');
            List<string> currentFileExtensions = new List<string>();
            bool isRight = false;
            if (value != null)
            {
                IFormFile file=(IFormFile)value;
               // HttpPostedFileBase file = (HttpPostedFileBase)value;
            
                string fileExtension = Path.GetExtension(file.FileName);
                
                if (fileExtension == ".zip")
                {
                    //using (var zip = new ZipArchive(file.InputStream, ZipArchiveMode.Read))
                    //{
                   // var zip = new ZipArchive(file.InputStream, ZipArchiveMode.Read);
                    var zip = new ZipArchive( file.OpenReadStream(), ZipArchiveMode.Read);
                    foreach (var entry in zip.Entries)
                    {
                        currentFileExtensions.Add(Path.GetExtension(entry.Name));
                    }
                    //}

                    foreach (var item in currentFileExtensions)
                    {
                        // || item == ".prj" || item == ".qpj" || item == ".sbn" || item == ".sbx" || item == ".shp" || item == ".shx"
                        arrExtensions.Contains(item);
                        if (item == ".prj")
                        {
                            cprj++;
                        }
                        else if (item == ".qpj")
                        {
                            cqpj++;
                        }
                        else if (item == ".shx")
                        {
                            cshx++;
                        }
                        else if (item == ".sbn")
                        {
                            csbn++;
                        }
                        else if (item == ".sbx")
                        {
                            csbx++;
                        }
                        else if (item == ".shp")
                        {
                            cshp++;
                        }
                        else if (item == ".dbf")
                        {

                            cdbf++;
                        }

                    }

                    if (cprj == 1 && cqpj == 1 && cshx == 1 && csbx == 1 && cshp == 1 && cdbf == 1 && csbn == 1)
                    {
                        isRight = true;


                    }

                    if (isRight)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Please upload a Valid zip file.");
                    }
                }
                else if (fileExtension == ".json")
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Please upload a Valid zip file.");
                }
            }
            else
            {
                return ValidationResult.Success;
            }

        }

    }
}
