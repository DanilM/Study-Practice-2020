using System;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Windows.Documents;
using System.Windows.Media;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Practice
{
  public partial class MainWindow : Window
  {
    Parser.Theatre[] theatres;

    private void FindContent()
    {
      for (int i = 0; i < theatres.Length; i++)
      {
        if ((FindText.Text == "") || (theatres[i].CommonName.ToLower().IndexOf(FindText.Text.ToLower()) != -1))
        {
          Button myButton = new Button
          {
            Name = "Button" + Convert.ToString(i),

          };
          myButton.Click += Button_Click;
          myButton.Tag = i;



          Grid myGrid = new Grid
          {
            Name = "Grid" + Convert.ToString(i),
            Height = 125,

          };
          RowDefinition rowDefinition1 = new RowDefinition();
          RowDefinition rowDefinition2 = new RowDefinition();
          myGrid.RowDefinitions.Add(rowDefinition1);
          myGrid.RowDefinitions.Add(rowDefinition2);
          rowDefinition1.Height = new GridLength(1, GridUnitType.Star);
          rowDefinition2.Height = new GridLength(2, GridUnitType.Star);

          TextBlock TextHeader = new TextBlock();
          TextBlock TextContent = new TextBlock();

          TextHeader.Inlines.Add(new Bold(new Run(theatres[i].CommonName)));
          TextContent.Inlines.Add(new Run(theatres[i].ObjectAddress[0].District + ", " + theatres[i].ObjectAddress[0].Address));

          myGrid.Children.Add(TextHeader);
          myGrid.Children.Add(TextContent);
          Grid.SetRow(TextHeader, 0);
          Grid.SetRow(TextContent, 1);

          myButton.Content = myGrid;

          Con.Children.Add(myButton);
        }
      }
    }


    private void SetContent()
    {
      string path = "data-20151225T0100.json";
      if (path.Substring(path.Length - 4) == "json")
      { 
        theatres = JsonConvert.DeserializeObject<Parser.Theatre[]>(File.ReadAllText(path, Encoding.UTF8));
        for (int i = 0; i < theatres.Length; i++)
        {

          Button myButton = new Button
          {
            Name = "Button" + Convert.ToString(i),
            
          };
          myButton.Click += Button_Click;
          myButton.Tag = i;



          Grid myGrid = new Grid
          {
            Name = "Grid" + Convert.ToString(i),
            Height = 125,

          };
          RowDefinition rowDefinition1 = new RowDefinition();
          RowDefinition rowDefinition2 = new RowDefinition();
          myGrid.RowDefinitions.Add(rowDefinition1);
          myGrid.RowDefinitions.Add(rowDefinition2);
          rowDefinition1.Height = new GridLength(1, GridUnitType.Star);
          rowDefinition2.Height = new GridLength(2, GridUnitType.Star);

          TextBlock TextHeader = new TextBlock();
          TextBlock TextContent = new TextBlock();

          TextHeader.Inlines.Add(new Bold(new Run(theatres[i].CommonName)));
          TextContent.Inlines.Add(new Run(theatres[i].ObjectAddress[0].District + ", " + theatres[i].ObjectAddress[0].Address));

          myGrid.Children.Add(TextHeader);
          myGrid.Children.Add(TextContent);
          Grid.SetRow(TextHeader, 0);
          Grid.SetRow(TextContent, 1);

          myButton.Content = myGrid;

          Con.Children.Add(myButton);
        }
      }
    }
    public MainWindow()
    {
      InitializeComponent();
      SetContent();
    }

    private void GoToSite(object sender, RoutedEventArgs e)
    {
      Hyperlink thisLink = (Hyperlink)sender;
      int ind = Convert.ToInt32(thisLink.Tag);
      Process.Start("www." + theatres[ind].WebSite);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      while (Con.Children.Count > 0)
      {
        Con.Children.RemoveAt(Con.Children.Count - 1);
      }
      Button thisButton = (Button)sender;
      Grid thisGrid = new Grid();
      thisGrid.Height = 370;

      int ind = Convert.ToInt32(thisButton.Tag);
      
      RowDefinition thisDefinition1 = new RowDefinition();
      RowDefinition thisDefinition2 = new RowDefinition();
      thisDefinition1.Height = new GridLength(50);
      thisDefinition2.Height = new GridLength(4, GridUnitType.Star);

      thisGrid.RowDefinitions.Add(thisDefinition1);
      thisGrid.RowDefinitions.Add(thisDefinition2);


      TextBlock Header = new TextBlock();
      TextBlock Content = new TextBlock();

      Hyperlink Site = new Hyperlink();
      Site.Tag = ind;
      Site.Inlines.Add(new Run(theatres[ind].WebSite));
      Site.Click += GoToSite;

      Header.Inlines.Add(new Bold(new Run(theatres[ind].CommonName)));
      Header.Inlines.Add(new LineBreak());
      Header.Inlines.Add(new Run(theatres[ind].ObjectAddress[0].District + ", " + theatres[ind].ObjectAddress[0].Address));

      Content.Inlines.Add(new Run(theatres[ind].Category + " "));
      Content.Inlines.Add(new Run(theatres[ind].ShortName));
      Content.Inlines.Add(new LineBreak());
      Content.Inlines.Add(new Run("Контактный телефон: " + theatres[ind].PublicPhone[0].PublicPhone[0]));
      Content.Inlines.Add(new LineBreak());
      Content.Inlines.Add(new Run("Сайт "));
      Content.Inlines.Add(Site);
      Content.Inlines.Add(new LineBreak());
      Content.Inlines.Add(new Run("Вместимость основного зала: " + theatres[ind].MainHallCapacity));
      Content.Inlines.Add(new LineBreak());
      Content.Inlines.Add(new Run("Вместимость дополнительного зала: " + theatres[ind].AdditionalHallCapacity));

      Grid workGrid = new Grid
      {
        Height = 200,
        Width = 310,
      };
      Thickness th = new Thickness(10);
      workGrid.Margin = th;
      workGrid.HorizontalAlignment = HorizontalAlignment.Right;
      workGrid.VerticalAlignment = VerticalAlignment.Bottom;
       
      ColumnDefinition columnDefinition1 = new ColumnDefinition();
      ColumnDefinition columnDefinition2 = new ColumnDefinition();
      columnDefinition1.Width = new GridLength(2,GridUnitType.Star);
      RowDefinition rowDefinition1 = new RowDefinition();
      TextBlock rasp = new TextBlock();
      rasp.Inlines.Add(new Bold(new Run("Расписание")));
      rasp.HorizontalAlignment = HorizontalAlignment.Center;

      workGrid.ColumnDefinitions.Add(columnDefinition1);
      workGrid.ColumnDefinitions.Add(columnDefinition2);
      
      workGrid.RowDefinitions.Add(rowDefinition1);
      workGrid.Children.Add(rasp);
      Grid.SetColumnSpan(rasp,2);

      for (int j = 0; j < 7; j++)
      {
        TextBlock day = new TextBlock();
        day.Inlines.Add(new Run(theatres[ind].WorkingHours[j].DayWeek));
        TextBlock hours = new TextBlock();
        hours.Inlines.Add(new Run(theatres[ind].WorkingHours[j].WorkHours));
        RowDefinition rowDefinition = new RowDefinition();
        workGrid.Children.Add(day);
        workGrid.Children.Add(hours);
        workGrid.RowDefinitions.Add(rowDefinition);
        Grid.SetRow(day, j + 1);
        Grid.SetRow(hours, j + 1);
        Grid.SetColumn(day, 0);
        Grid.SetColumn(hours, 1);
      }

      thisGrid.Children.Add(Header);
      thisGrid.Children.Add(Content);
      thisGrid.Children.Add(workGrid);

      Grid.SetRow(Header, 0);
      Grid.SetRow(Content, 1);
      Grid.SetRow(workGrid, 1);

      Con.Children.Add(thisGrid);
    }

    private void Button_Back(object sender, RoutedEventArgs e)
    {
      while (Con.Children.Count > 0)
      {
        Con.Children.RemoveAt(Con.Children.Count - 1);
      }
      FindText.Text = "";
      SetContent();
    }

    private void Button_Find(object sender, RoutedEventArgs e)
    {
      while (Con.Children.Count > 0)
      {
        Con.Children.RemoveAt(Con.Children.Count - 1);
      }
      FindContent();
    }

    private void FindText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.Return)
      {
        while (Con.Children.Count > 0)
        {
          Con.Children.RemoveAt(Con.Children.Count - 1);
        }
        FindContent();
      }
    }
  }
}
