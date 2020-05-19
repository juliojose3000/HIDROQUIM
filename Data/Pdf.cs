
using HIDROQUIM.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Data
{
    public class Pdf
    {
       
        public Pdf()
        {

            

        }

        public void generarPdf(int id) {
            AnalisisSueloDao analisisSueloDao = new AnalisisSueloDao();
            AnalisisQuimicoSuelo analisisSuelo = analisisSueloDao.getAnalisisSueloById(id);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\ajosu\Documents\HIDROQUIM\HIDROQUIM\Documentos\prueba2.pdf", FileMode.Create));
            doc.Open();
            //Pagina 1
            // Creamos una tabla de encabezado
            PdfPTable tblPrueba = new PdfPTable(3);
            tblPrueba.WidthPercentage = 100;

            //Crea imagen
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Users/ajosu/Documents/HIDROQUIM/HIDROQUIM/Images/logo.jpg");
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_LEFT;
            float percentage = 0.0f;
            percentage = 150 / imagen.Width;
            imagen.ScalePercent(percentage * 100);

            // Configuramos el título de las columnas de la tabla
            PdfPCell clImagen = new PdfPCell(imagen);
            tblPrueba.AddCell(clImagen);

            Paragraph reporteResultadosTabla = new Paragraph("Reporte de Resultados");
            reporteResultadosTabla.Alignment = Element.ALIGN_CENTER;

            PdfPCell clNombre = new PdfPCell();
            clNombre.AddElement(new Paragraph(reporteResultadosTabla));
            clNombre.AddElement(new Phrase("Asesoría y Monitoreo Ambiental Hidroquim S.A"));
            tblPrueba.AddCell(clNombre);

            PdfPCell clApellido = new PdfPCell();
            clApellido = new PdfPCell();
            clApellido.AddElement(new Phrase("Codigo: "+analisisSuelo.IdSuelo));
            clApellido.AddElement(new Phrase("Fecha de Emisión: "+analisisSuelo.AnalisisQuimico.FechaMuestreo));
            clApellido.AddElement(new Phrase("Consecutivo: "+analisisSuelo.AnalisisQuimico.Codigo));
            clApellido.AddElement(new Phrase("Pagina 1"));
            tblPrueba.AddCell(clApellido);

            doc.Add(tblPrueba);

            Paragraph reporteResultados = new Paragraph("Reporte de Resultados");
            reporteResultados.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            reporteResultados.Alignment = Element.ALIGN_CENTER;
            doc.Add(reporteResultados);

            Paragraph cliente = new Paragraph("Nombre del cliente: "+analisisSuelo.AnalisisQuimico.Cliente.Nombre_cliente);
            cliente.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            cliente.SetLeading(3,3);
            doc.Add(cliente);

            Paragraph clienteDireccion = new Paragraph("Dirección del cliente");
            clienteDireccion.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            clienteDireccion.SetLeading(3, 3);
            doc.Add(clienteDireccion);

            Paragraph telefono = new Paragraph("Teléfono: ");
            telefono.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            telefono.SetLeading(3, 3);
            doc.Add(telefono);

            Paragraph correo = new Paragraph("Correo electrónico");
            correo.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            correo.SetLeading(3, 3);
            doc.Add(correo);

            Paragraph codigoSolicitud = new Paragraph("Código de la solicitud de servicio");
            codigoSolicitud.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            codigoSolicitud.SetLeading(3, 3);
            doc.Add(codigoSolicitud);

            Paragraph muestreado = new Paragraph("Muestreado por Leonardo Corrales");
            muestreado.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            muestreado.SetLeading(3, 3);
            doc.Add(muestreado);

            Paragraph procedimiento = new Paragraph("Procedimiento de muestreo");
            procedimiento.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            procedimiento.SetLeading(3, 3);
            doc.Add(procedimiento);

            Paragraph fechaMuestreo = new Paragraph("Fecha de muestreo: "+analisisSuelo.AnalisisQuimico.FechaMuestreo);
            fechaMuestreo.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            fechaMuestreo.SetLeading(3, 3);
            doc.Add(fechaMuestreo);

            Paragraph fechaAnalisis = new Paragraph("Fecha de análisis: "+analisisSuelo.AnalisisQuimico.FechaResultado);
            fechaAnalisis.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            fechaAnalisis.SetLeading(3, 3);
            doc.Add(fechaAnalisis);

            Paragraph fechaEmision = new Paragraph("Fecha de emisión del reporte: "+ DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
            fechaEmision.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            fechaEmision.SetLeading(3, 3);
            doc.Add(fechaEmision);

            Paragraph metodologias = new Paragraph("Metodologías de análisis empleadas");
            metodologias.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            metodologias.SetLeading(10, 3);
            doc.Add(metodologias);

            //Pagina 2
            // Creamos una tabla de encabezado
            PdfPTable tblPrueba2 = new PdfPTable(3);
            tblPrueba.WidthPercentage = 100;
            
            // Configuramos el título de las columnas de la tabla
            clImagen = new PdfPCell(imagen);
            tblPrueba2.AddCell(clImagen);

            reporteResultadosTabla = new Paragraph("Reporte de Resultados");
            reporteResultadosTabla.Alignment = Element.ALIGN_CENTER;

            clNombre = new PdfPCell();
            clNombre.AddElement(new Paragraph(reporteResultadosTabla));
            clNombre.AddElement(new Phrase("Asesoría y Monitoreo Ambiental Hidroquim S.A"));
            tblPrueba2.AddCell(clNombre);

            clApellido = new PdfPCell();
            clApellido = new PdfPCell();
            clApellido.AddElement(new Phrase("Codigo"));
            clApellido.AddElement(new Phrase("Fecha de Emisión"));
            clApellido.AddElement(new Phrase("Consecutivo"));
            clApellido.AddElement(new Phrase("Pagina 2"));
            tblPrueba2.AddCell(clApellido);

            doc.Add(tblPrueba2);

            Paragraph textoCuadro = new Paragraph("Cuadro I: Resultado de análisis de parámetros universales para el vertido de aguas residuales a un cuerpo receptro.");
            textoCuadro.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            textoCuadro.SetLeading(3, 3);
            doc.Add(textoCuadro);

            //Tabla parametros
            PdfPTable table = new PdfPTable(3);

            table.AddCell("Parámetro");
            table.AddCell("Referencia");
            table.AddCell("Resultado");

            table.AddCell("- DBO5,20");
            table.AddCell("50 mg/L");
            table.AddCell(" ");

            table.AddCell("- DQO ");
            table.AddCell("150 mg/L");
            table.AddCell(" ");

            table.AddCell("- Sólidos suspendidos ");
            table.AddCell("50 mg/L");
            table.AddCell(" ");

            table.AddCell("- Grasas/aceites ");
            table.AddCell("30 mg/L");
            table.AddCell(" ");

            table.AddCell("- Potencial hidrógeno ");
            table.AddCell("5 a 9");
            table.AddCell(" ");

            table.AddCell("- Temperatura ");
            table.AddCell("15°C <= T <= 40°C");
            table.AddCell(" ");

            table.AddCell("- Sólidos sedimentales ");
            table.AddCell("1 mL/L");
            table.AddCell(" ");

            table.AddCell("- Sustancias activas al azul de metileno ");
            table.AddCell("5 mg/L");
            table.AddCell(" ");

            doc.Add(table);

            Paragraph textoBajoCuadro = new Paragraph("nd: no detectable"+
                            "La incertidumbre de la medición se determina con un factor de cobertura k = 2, con un nivel de confianza del 95 %."+
                            "Decreto Nº 33601 - MINAE - S: Reglamento de vertido y reuso de aguas residuales."+
                            "Analisis realizado por el Laboratorio de Análisis de Asesoría y Monitoreo Ambiental Hidroquím");
            textoBajoCuadro.Font = new Font(FontFactory.GetFont("Arial", 9, Font.BOLD));
            textoBajoCuadro.SetLeading(3, 3);
            doc.Add(textoBajoCuadro);

            Paragraph descripcion = new Paragraph("Descripción de las muestras");
            descripcion.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            descripcion.SetLeading(3, 3);
            doc.Add(descripcion);

            Paragraph notas = new Paragraph("Notas");
            notas.Font = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            notas.SetLeading(5, 5);
            doc.Add(notas);

            //tabla ultima

            PdfPTable table2 = new PdfPTable(3);

            PdfPCell elaborado = new PdfPCell();
            elaborado.AddElement(new Phrase("Elaborado por:"));
            elaborado.AddElement(new Phrase(" "));
            elaborado.AddElement(new Phrase("Cargo:"));
            elaborado.AddElement(new Phrase(" "));
            elaborado.AddElement(new Phrase("Firma:"));
            table2.AddCell(elaborado);

            elaborado = new PdfPCell();
            elaborado.AddElement(new Phrase("Revisado por:"));
            elaborado.AddElement(new Phrase(" "));
            elaborado.AddElement(new Phrase("Cargo:"));
            elaborado.AddElement(new Phrase(" "));
            elaborado.AddElement(new Phrase("Firma:"));
            table2.AddCell(elaborado);

            elaborado = new PdfPCell();
            elaborado.AddElement(new Phrase("Aprobado por:"));
            elaborado.AddElement(new Phrase(" "));
            elaborado.AddElement(new Phrase("Cargo:"));
            elaborado.AddElement(new Phrase(" "));
            elaborado.AddElement(new Phrase("Firma:"));
            table2.AddCell(elaborado);

            doc.Add(table2);

            doc.Close();
        }
    }
}