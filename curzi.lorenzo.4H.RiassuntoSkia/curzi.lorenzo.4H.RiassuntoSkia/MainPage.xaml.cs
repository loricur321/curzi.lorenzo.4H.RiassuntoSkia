using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace curzi.lorenzo._4H.RiassuntoSkia
{
    public partial class MainPage : ContentPage
    {
        //margini del foglio di quaderno
        const int MARGINE_SINISTRO = 100;
        const int MARGINE_DESTRO = 100;
        const int MARGINE_SUPERIORE = 100;
        const int MARGINE_INFERIORE = 100;

        //folgio di quanderno 20 x 30 cm
        const int LARGHEZZA_RETTANGOLO = 200;
        const int ALTEZZA_RETTANGOLO = 300;

        SKPath path = new SKPath(); //tella su cui disegnerò il folgio di quaderno

        public MainPage()
        {
            InitializeComponent();
        }

        private void btnDisegno_Clicked(object sender, EventArgs e)
        {
            //quando clicclo il pulsante richiamo il metodo per disegnare il folgio da quaderno
            disegnaRettangolo();
            //e inseguito quello che mi consente di disegnare una diagonale del rettangolo
            disegnaDiagonale();

            Tela.InvalidateSurface();
        }

        private void disegnaRettangolo()
        {
            //E (MargineSinistro, MArgineSuperiore)
            double xEdispositivo = MARGINE_SINISTRO;
            double yEdispositivo = MARGINE_SUPERIORE;
            
            //F (MArgineSinistro + 200, MargineSupeiore)
            double xFdispositivo = MARGINE_SINISTRO + LARGHEZZA_RETTANGOLO;
            double yFdispositivo = MARGINE_SUPERIORE;

            //G (MArgineSinistro + 200, MArgineSuperiore + 300)
            double xGdispositivo = MARGINE_SINISTRO + LARGHEZZA_RETTANGOLO;
            double yGdispositivo = MARGINE_SUPERIORE + ALTEZZA_RETTANGOLO;

            //H (MArgineSinistro, MArgineSuperiore + 300)
            double xHdispositivo = MARGINE_SINISTRO;
            double yHdispositivo = MARGINE_SUPERIORE + ALTEZZA_RETTANGOLO;

            //disegno il rettangolo utilizzando il metodo che mi consente di stampare un segmento
            disegnaSegmento(xEdispositivo, yEdispositivo, xFdispositivo, yFdispositivo);
            disegnaSegmento(xFdispositivo, yFdispositivo, xGdispositivo, yGdispositivo);
            disegnaSegmento(xGdispositivo, yGdispositivo, xHdispositivo, yHdispositivo);
            disegnaSegmento(xHdispositivo, yHdispositivo, xEdispositivo, yEdispositivo);
        }

        private void disegnaDiagonale()
        {
            //punto H (0,0)
            double xHdisegno = trasformaXfoglio(0);
            double yHdisegno = trasformaYfoglio(0);

            //punto F (200,300)
            double xFdisegno = trasformaXfoglio(200);
            double yFdisegno = trasformaYfoglio(300);

            disegnaSegmento(xHdisegno, yHdisegno, xFdisegno, yFdisegno);
        }

        //disegno un segmento tracciando una retta da un punto a un altro
        private void disegnaSegmento(double x1, double y1, double x2, double y2)
        {
            //creo dei punti skia
            SKPoint p1 = new SKPoint((float)x1, (float)y1);
            SKPoint p2 = new SKPoint((float)x2, (float)y2);
            
            //e poi li stampo
            path.MoveTo(p1);
            path.LineTo(p2);
        }

        private void Tela_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;

            //prelevo la tela e la cancello
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            //pennello verde
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Green,
                StrokeWidth = 3
            };

            canvas.DrawPath(path, paint);
        }

        //converto la x in una coordinata che sta all'interno del folgio di quaderno
        private double trasformaXfoglio(double x)
        {
            return MARGINE_SINISTRO + x;
        }

        //converto la y in una coordinata che sta all'interno del folgio di quaderno
        private double trasformaYfoglio(double y)
        {
            return (MARGINE_SUPERIORE + ALTEZZA_RETTANGOLO) - y;
        }
    }
}
