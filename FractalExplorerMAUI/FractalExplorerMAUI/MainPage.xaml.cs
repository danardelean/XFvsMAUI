using System.Diagnostics;
using System.Numerics;
using FractlExplorer;

namespace FractalExplorerMAUI;

public partial class MainPage : ContentPage
{
    const int maxIterations = 1000;

    Complex center = new Complex(0, 0), moving;
    double scale = 3.0, pinchScale;
    int[] colors = { Colors.Black.ToABGR(), Colors.Red.ToABGR(), Colors.Blue.ToABGR(), Colors.Yellow.ToABGR(), Colors.Green.ToABGR() };
    int[] memoryBuffer;
    double viewWidth, viewHeight;
    FractlExplorer.Utility.IImageCreator _ic;
    public MainPage(FractlExplorer.Utility.IImageCreator ic)
    {
        InitializeComponent();
        _ic = ic;
    }

    Complex FromPoint(Point p)
    {
        double increment = scale / viewWidth;
        Point centerPixel = new Point(viewWidth / 2, viewHeight / 2);
        double xdelta = p.X - centerPixel.X;
        double ydelta = p.Y - centerPixel.Y;
        return center + new Complex(xdelta * increment, ydelta * increment);
    }

    void OnReset(object sender, EventArgs e)
    {
        center = new Complex(0, 0);
        scale = 3.0;

        StartRender();
    }

    private void StartRender()
    {
        GenerateFractl(maxIterations);
    }

    private void GenerateFractl(int maxIterations)
    {
        Stopwatch sw = Stopwatch.StartNew();

        FractlExplorer.Mandelbrot mandelbrotGenerator = new FractlExplorer.Mandelbrot();

        double increment = scale / viewWidth;
        double left = center.Real - (viewWidth / 2) * increment;
        double row = center.Imaginary - (viewHeight / 2) * increment;

        Console.WriteLine($"Doing {viewHeight} Render Rows Left:{left} Increment:{increment} Row:{row}");
        for (int y = 0; y < viewHeight; y++)
        {
            mandelbrotGenerator.RenderRow(left, row, increment,
                maxIterations, colors, memoryBuffer, (int)(y * viewWidth), (int)viewWidth);
            row += increment;
        }

        RefreshScreen();

        sw.Stop();
        timer.Text = sw.Elapsed.ToString();
    }

    void RefreshScreen()
    {
        Task<Stream> t = _ic.CreateAsync(memoryBuffer, (int)viewWidth, (int)viewHeight);

        this.imageHost.Source = ImageSource.FromStream(() => t.Result);
        btnGenerateMandelbrot.IsEnabled = true;
    }




    void btnGenerateMandelbrot_Clicked(System.Object sender, System.EventArgs e)
    {
        btnGenerateMandelbrot.IsEnabled = false;
        viewWidth = Width * _ic.ScaleFactor;
        viewHeight = Height * _ic.ScaleFactor;
        memoryBuffer = new int[(int)(viewWidth * viewHeight)];
        imageHost.WidthRequest = Width;
        imageHost.HeightRequest = Height;
        imageHost.Source = null;
        StartRender();
    }
}


