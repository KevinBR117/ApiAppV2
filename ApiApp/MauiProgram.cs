﻿
namespace ApiApp;
using ApiApp.Models;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		string dbName = "picture.db3";
		builder.Services.AddSingleton<PictureRepository>(s => ActivatorUtilities.CreateInstance<PictureRepository>(s, dbName));
		return builder.Build();
	}
}