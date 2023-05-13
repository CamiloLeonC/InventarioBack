using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using BackSistemaUbala.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class NotaManager : INotaManager
    {
        private readonly AplicationDbContext context;

        public NotaManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<NotaModel> GetAll(string userId, string userRol)
        {
            if(userRol == "Alumno")
            {
                return context.Notas.Where(x => x.IdUser == userId);
            }
            else
            {
                return context.Notas;
            }
        }

        public NotaModel GetById(int keyNotaModelId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            NotaModel result = null;

            try
            {
                result = context.Notas.Where((x) => x.Id == keyNotaModelId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Nota no encontrada");
            }

            return result;
        }

        public NotaModel Add(NotaModel row)
        {
            NotaModel result = null;
            try
            {
                result = context.Notas.FirstOrDefault(x => x.Id == row.Id);
                if (result != null)
                {
                    throw new Exception("Id de la nota duplicado.");
                }
                else
                {
                    if (existeNota(row.Id, row.IdMateriaProfesor, row.IdUser, row.Periodo)) throw new Exception("Ya existe una nota para ese alumno en es materia dentro de ese periodo.");
                    result = row;
                    context.Add(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public NotaModel Delete(int keyNotaModelId)
        {
            NotaModel result = null;
            try
            {
                result = context.Notas.FirstOrDefault(x => x.Id == keyNotaModelId);
                if (result == null)
                {
                    throw new Exception("Nota no encontrada");
                }
                else
                {
                    context.Notas.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public NotaModel Update(int keyNotaModelId, NotaModel changes)
        {
            NotaModel result = null;
            try
            {
                result = context.Notas.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.IdMateriaProfesor = changes.IdMateriaProfesor;
                    result.IdAlumno = changes.IdAlumno;
                    result.Periodo = changes.Periodo;
                    result.NotaSaber = changes.NotaSaber;
                    result.NotaHacer = changes.NotaHacer;
                    result.NotaSer = changes.NotaSer;
                    result.DefinitivaTotal = changes.DefinitivaTotal;
                    result.Observaciones = changes.Observaciones;
                    if (existeNota(changes.Id, changes.IdMateriaProfesor, changes.IdUser, changes.Periodo)) throw new Exception("Ya existe una nota para ese alumno en es materia dentro de ese periodo.");
                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró registro de nota");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public string CargarExcel(string filePath)
        {
            string messageError = "";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = null;
            var materias = context.Materias.ToList();
            var usuarios = context.ApplicationUsers.ToList();
            var materiasP = context.MateriaProfesores.ToList();
            var materiaProfesor = new MateriaProfesorModel();


            List<NotaModel> notasAlumnos = new List<NotaModel>();

            try
            {
                using (package = new ExcelPackage(new FileInfo(filePath)))
                {
                    //Se valida la cantidad de columnas correspondiente para el proceso
                    var worksheet = ValidateStructureFile(filePath, package, 11);

                    var totalRows = worksheet.Dimension.End.Row;
                    for (var row = 2; row <= totalRows; row++) //comenzar en 2 para saltarnos la fila del header
                    {
                        int column = 0;
                        if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text.Trim()))
                            //salimos si el excel tiene filas en blanco
                            break;
                        var docAlumno = worksheet.Cells[row, ++column].Text.Trim(); //Trim elimina espacios de inicio y de fin en el registro
                        var alumno = usuarios.FirstOrDefault(x => x.Documento == docAlumno); //Linq
                        if (alumno == null)
                        {
                            messageError += $"No existe un alumno con ese documento, fila: {row} columna: {column}. \n";
                        }
                        //Columna es igual a 2
                        column = ++column + 1;
                        var codMateria = worksheet.Cells[row, column].Text.Trim(); //Trim elimina espacios de inicio y de fin en el registro
                        var materia = materias.FirstOrDefault(x => x.Cod == codMateria); //Linq
                        if (materia == null)
                        {
                            messageError += $"No existe una materia con ese código, fila: {row} columna: {column}. \n";
                        }
                        //Columna es igual a 5
                        column = ++column + 1;
                        var docProfesor = worksheet.Cells[row, column].Text.Trim(); //Trim elimina espacios de inicio y de fin en el registro
                        var profesor = usuarios.FirstOrDefault(x => x.Documento == docProfesor); //Linq
                        if (profesor == null)
                        {
                            messageError += $"No existe un profesor con ese Documento, fila: {row} columna: {column}. \n";
                        }
                        //Coluna es igual a 7
                        column = ++column + 1;
                        var periodo = worksheet.Cells[row, column].Text.Trim(); //Trim elimina espacios de inicio y de fin en el registro
                        if (string.IsNullOrEmpty(periodo))
                        {
                            messageError += $"Periodo Obligatorio, fila: {row} columna: {column}. \n";
                        }
                        var selectedPeriodo = new Models.Enums.Periodo();
                        switch (periodo)
                        {
                            case "Primero":
                                selectedPeriodo = Models.Enums.Periodo.Primer;
                                break;
                            case "Segundo":
                                selectedPeriodo = Models.Enums.Periodo.Segundo;
                                break;
                            case "Tercero":
                                selectedPeriodo = Models.Enums.Periodo.Tercer;
                                break;
                            case "Cuarto":
                                selectedPeriodo = Models.Enums.Periodo.Cuarto;
                                break;
                            default:
                                selectedPeriodo = 0;
                                messageError += $"No existe ese periodo, fila: {row} columna: {column}. \n";
                                break;
                        }
                        var defSaber = worksheet.Cells[row, ++column].Text.Trim(); //Trim elimina espacios de inicio y de fin en el registro
                        if(string.IsNullOrEmpty(defSaber))
                        {
                            messageError += $"Obligatorio, fila: {row} columna: {column}. \n";
                        }
                        else
                        {
                            if (decimal.Parse(defSaber) < 0 && decimal.Parse(defSaber) > 5)
                            {
                                messageError += $"Nota inválida, fila: {row} columna: {column}. \n";

                            }
                        }
                        var defHacer = worksheet.Cells[row, ++column].Text.Trim(); //Trim elimina espacios de inicio y de fin en el registro
                        if (string.IsNullOrEmpty(defHacer))
                        {
                            messageError += $"Obligatorio, fila: {row} columna: {column}. \n";
                        }
                        else
                        {
                            if (decimal.Parse(defHacer) < 0 && decimal.Parse(defHacer) > 5)
                            {
                                messageError += $"Nota inválida, fila: {row} columna: {column}. \n";

                            }
                        }
                        var defSer = worksheet.Cells[row, ++column].Text.Trim(); //Trim elimina espacios de inicio y de fin en el registro
                        if (string.IsNullOrEmpty(defSer))
                        {
                            messageError += $"Obligatorio, fila: {row} columna: {column}. \n";
                        }
                        else
                        {
                            if (decimal.Parse(defSer) < 0 && decimal.Parse(defSer) > 5)
                            {
                                messageError += $"Nota inválida, fila: {row} columna: {column}. \n";

                            }
                        }
                        var observacion = worksheet.Cells[row, ++column].Text.Trim(); //Trim elimina espacios de inicio y de fin en el registro
                        if (string.IsNullOrEmpty(defSer))
                        {
                            messageError += $"Obligatorio, fila: {row} columna: {column}. \n";
                        }

                        // Consultar Materia Profesor
                        if (profesor != null && materia != null)
                        {
                            materiaProfesor = materiasP.FirstOrDefault(x => x.IdMateria == materia.Id && x.IdUser == profesor.Id);
                            if (materiaProfesor == null)
                            {
                                messageError += $"No existe un profesor con esa materia en el sistema. \n";
                            }
                        }

                        if (materiaProfesor != null && alumno != null)
                        {
                            if (context.Notas.Any(x => x.IdUser == alumno.Id && x.IdMateriaProfesor == materiaProfesor.Id && x.Periodo == selectedPeriodo))
                            {
                                messageError += $"Ya se registro esa Nota en esa materia y con ese periodo. \n";
                            }
                        }
                        

                        if (string.IsNullOrEmpty(messageError))
                        {
                            NotaModel nota = new NotaModel();
                            nota.IdUser = alumno.Id;
                            nota.IdMateriaProfesor = materiaProfesor.Id;
                            nota.Periodo = selectedPeriodo;
                            nota.NotaSaber = decimal.Parse(defSaber);
                            nota.NotaHacer = decimal.Parse(defHacer);
                            nota.NotaSer = decimal.Parse(defSer);
                            nota.Observaciones = observacion;
                            nota.DefinitivaTotal = nota.NotaSaber + nota.NotaHacer + nota.NotaSer;

                            notasAlumnos.Add(nota);
                        }

                    }
                    if (string.IsNullOrEmpty(messageError))
                    {
                        context.Notas.AddRange(notasAlumnos);
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                messageError += ex.Message;
            }
            return messageError;
        }

        private static ExcelWorksheet ValidateStructureFile(string selectedFileName, ExcelPackage package, int totalExpectedColumns, int sheet = 0)
        {
            //Validar que el fichero existe
            if (!File.Exists(selectedFileName))
                throw new FileNotFoundException(selectedFileName);

            if (sheet > 0 && sheet > package.Workbook.Worksheets.Count - 1)
                throw new DataMisalignedException($"La hoja número {sheet} no existe dentro del archivo.");

            var worksheet = package.Workbook.Worksheets[sheet];

            #region Validate File
            //validar total de columnas
            var totalColumns = worksheet.Dimension.End.Column;
            if (totalColumns != totalExpectedColumns)
                throw new DataMisalignedException($"La cantidad de columnas esperadas es {totalExpectedColumns} en la hoja {worksheet.Name}.");

            #endregion

            return worksheet;
        }
        public byte[] ExportarNotas()
        {
            var excelStream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            byte[] data = null;

            using (var package = new ExcelPackage(excelStream))
            {
                var worksheet = package.Workbook.Worksheets.Add($"Auditoria");
                int filaActual = 5;

                worksheet.Cells[1, 1].Value = "Fecha generación";
                worksheet.Cells[1, 2].Value = DateTime.Today.ToShortDateString();

                worksheet.Cells[filaActual, 1].Value = "ALUMNO";
                worksheet.Cells[filaActual, 2].Value = "MATERIA POR PROFESOR";
                worksheet.Cells[filaActual, 3].Value = "PERIODO";
                worksheet.Cells[filaActual, 4].Value = "DEFINITIVA TOTAL";
                worksheet.Cells[filaActual, 5].Value = "OBSERVACIONES";
         

                //Ponemos los encabezados en negrita
                worksheet.Row(filaActual).Style.Font.Bold = true;

                //Ponemos los encabezados centrados
                worksheet.Row(filaActual).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var resultado = context.Notas
                    .Include(x => x.ApplicationUser)
                    .Include(y => y.MateriaProfesor).ThenInclude(y => y.Materia)
                    .Include(y => y.MateriaProfesor).ThenInclude(y => y.ApplicationUser).ToArray();


                foreach (var result in resultado)
                {
                    filaActual++;
                    worksheet.Cells[filaActual, 1].Value = result.ApplicationUser.NombreCompleto;
                    worksheet.Cells[filaActual, 2].Value = result.MateriaProfesor.Materia.Nombre + " por " + result.MateriaProfesor.ApplicationUser.NombreCompleto;
                    worksheet.Cells[filaActual, 3].Value = result.Periodo;
                    worksheet.Cells[filaActual, 4].Value = result.DefinitivaTotal;
                    worksheet.Cells[filaActual, 5].Value = result.Observaciones;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                //set some properties
                package.Workbook.Properties.Title = $"Informe de notas";

                // set some extended property values
                package.Workbook.Properties.Company = "Ubala";

                //obtenemos el arreglo de bytes para exportar
                data = package.GetAsByteArray();
            }

            return data;
        }

        private bool existeNota(int Id, int IdMateriaProfesor, string IdUser, Periodo periodo)
        {
            return context.Notas.Any(x => x.Id != Id && x.IdMateriaProfesor == IdMateriaProfesor && x.IdUser == IdUser && x.Periodo == periodo);
        }
    }
}
