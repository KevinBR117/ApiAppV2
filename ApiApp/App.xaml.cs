using ApiApp.Models;
namespace ApiApp;

public partial class App : Application
{
	public static PictureRepository PictureRepo { get; set; }
	public App(PictureRepository repo)
	{
		InitializeComponent();

		MainPage = new AppShell();

		PictureRepo = repo;
	}
}
