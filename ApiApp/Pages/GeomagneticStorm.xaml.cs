using System.Text.Json;
using System.Collections.Generic;
namespace ApiApp.Pages;

public partial class GeomagneticStorm : ContentPage
{
	public GeomagneticStorm()
	{
		InitializeComponent();

		InitializeUI();
	}

	void InitializeUI()
	{
		entInicio.Text = "2023-11-01";
		entFinal.Text = DateTime.Today.ToString("yyyy-MM-dd");
	}

	public async void OnConsultarButtonClicked(object sender, EventArgs e)
	{
		//string fecha_inicial = entInicio.Text;
		//string fecha_final = entFinal.Text;

        string url = string.Format("https://kauai.ccmc.gsfc.nasa.gov/DONKI/WS/get/GST?startDate={0}&endDate={1}",
               entInicio.Text,
               entFinal.Text);

        var client = new HttpClient();
		var request = new HttpRequestMessage
		{
			Method = HttpMethod.Get,
			RequestUri = new Uri(url)
		};
		using (var response = await client.SendAsync(request))
		{
			response.EnsureSuccessStatusCode();
			var body = await response.Content.ReadAsStringAsync();
			Console.WriteLine(body);
			var geoStorm = JsonSerializer.Deserialize<List<GeoStrom>>(body);
			//GeoStrom? geoStorm = JsonSerializer.Deserialize<GeoStrom>(body);
			//var geoStormObject = JsonSerializer.Deserialize<GeoStormObject>(body);

			//Console.WriteLine(geoStormObject.geoStorm[0]);

			//Console.WriteLine(geoStorm);
			Console.WriteLine(geoStorm[0]);
			Console.WriteLine(geoStorm[0].startTime);
			Console.WriteLine(geoStorm[0].allKpIndex[0].kpIndex);

			for (int i = 0; i < geoStorm[0].allKpIndex.Count; i++)
			{
				gridKpIndex.RowDefinitions.Add(new RowDefinition { });
				gridKpIndex.ColumnDefinitions.Add(new ColumnDefinition { });
				gridKpIndex.Add(new Label {Text = string.Format("Observed time: {0}", DateTime.Parse(geoStorm[0].allKpIndex[i].observedTime).ToString("MM/dd/yyyy HH:mm"))}, 0, i);
				gridKpIndex.Add(new Label { Text = string.Format("Kp index: {0}", geoStorm[0].allKpIndex[i].kpIndex)}, 1, i);
			}

			//Label lblObservedTime = new Label
			//{
			//	Text = string.Format("Observed time: {0}", geoStorm[0].allKpIndex[0].observedTime)
			//};
			
			//Label lblKpIndex = new Label {
			//	Text = string.Format("Kp index: {0}", geoStorm[0].allKpIndex[0].kpIndex)
			//};

   //         Label lblObservedTime1 = new Label
   //         {
   //             Text = string.Format("Observed time: {0}", geoStorm[0].allKpIndex[0].observedTime)
   //         };

   //         Label lblKpIndex1 = new Label
   //         {
   //             Text = string.Format("Kp index: {0}", geoStorm[0].allKpIndex[0].kpIndex)
   //         };

   //         gridKpIndex.Add(lblObservedTime, 0, 0); //col index, row index
   //         gridKpIndex.Add(lblKpIndex, 1, 0);

   //         gridKpIndex.Add(lblObservedTime1, 0, 1); //col index, row index
   //         gridKpIndex.Add(lblKpIndex1, 1, 1);

            //Console.WriteLine(geoStorm[1]);
            //Console.WriteLine(geoStorm[1].gstID);

            //lblKpIndex.Text = String.Format("{0}", geoStorm.Count);
        }

    }

	//public class GeoStormObject
	//{
	//	public GeoStrom[] geoStorm { get; set; }
	//}

	public class GeoStrom
	{
		public string gstID { get; set; }
		public string startTime {get; set;}
		public List<KpIndexTemps> allKpIndex { get; set; }

		//public KpIndexTemps[] allKpIndex { get; set; }
		
	}

	public class KpIndexTemps
	{
		public string observedTime { get; set; }
		public decimal kpIndex { get; set; }
		public string source { get; set; }
	} 
}