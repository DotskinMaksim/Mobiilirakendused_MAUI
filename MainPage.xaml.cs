namespace MAUI_project;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();


		CarouselView carouselView=new CarouselView
		{
			VerticalOptions=LayoutOptions.Center,
		};

		carouselView.ItemsSource = new List<Product> 
		{ 
			new Product{Name="Carrot",Description="Dick1",Image="carrot.jpg"},
			new Product{Name="Cucmber",Description="Dick2",Image="cucumber.jpg"},
			new Product{Name="Tomato",Description="Dick3",Image="tomato.jpg"},

        };
		carouselView.ItemsLayout=new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
        carouselView.ItemTemplate = new DataTemplate(() =>
        {
            Label header = new Label
            {
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 18
            };
            header.SetBinding(Label.TextProperty, "Name");

            Image image = new Image { WidthRequest = 150, HeightRequest = 150 };
            image.SetBinding(Image.SourceProperty, "Image"); 

            Label description = new Label { HorizontalTextAlignment = TextAlignment.Center };
            description.SetBinding(Label.TextProperty, "Description");

            StackLayout stackLayout = new StackLayout { Children = { header, image, description } };
            Frame frame = new Frame { Content = stackLayout };

			return frame;
        });

        Content = carouselView;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{

		Random random= new Random();

		int r, g, b;
		r=random.Next(1,255);
		g=random.Next(1,255);
		b=random.Next(1,255);
        CounterBtn.BackgroundColor = Color.FromRgb(r,g,b);


		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

