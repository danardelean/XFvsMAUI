namespace FractalExplorerMAUI;

public partial class App : Application
{
	public App(FractlExplorer.Utility.IImageCreator ic)
	{
		InitializeComponent();

		MainPage = new MainPage(ic);
	}
}

