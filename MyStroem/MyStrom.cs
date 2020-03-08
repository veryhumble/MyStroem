using System;

namespace MyStroem
{
    public class MyStrom
    {   
        public string Name { get; set; }
        
        public string Address { get; set; }

        public Uri ReportUrl => new Uri($"http://{Address}/report");

        public Report Report { get; set; }
        
    }

    public struct Report
    {
        public double Power { get; set; }

        public bool Relay { get; set; }
    }
}

<-- No Location Empty State -!->
<Grid Grid.Row="4" 
      IsVisible="{Binding HasLocation, Converter={StaticResource BooleanNegationConverter}}" 
      Padding="30, 35, 30, 35" 
      BackgroundColor="White"> 
  <Grid.RowDefinitions> 
    <RowDefinition Height="*" /> 
    <RowDefinition Height="50" /> 
  </Grid.RowDefinitions> 

  <StackLayout Grid.Row="0"> 
    <Image VerticalOptions="Start" 
           HorizontalOptions="Center" 
           Source="illustration_empty_content.png" /> 

    <controls1:LabelEx Style="{StaticResource HeadlineBlackBaseStyle}" 
                       HorizontalTextAlignment="Center" 
                       Text="{i18n:Translate WarningNoLocation}" 
                       Margin="0, 35, 0, 0"/> 

    <controls1:LabelEx Style="{StaticResource SubheadGrayBaseStyle}" 
                       HorizontalTextAlignment="Center" 
                       Text="{i18n:Translate WarningNoLocationDescription}" 
                       Margin="0, 0, 0, 35"/> 
  </StackLayout> 
  <controls1:ButtonEx Grid.Row="1" 
                      Text="{i18n:Translate SettingsButton}" 
                      Style="{StaticResource ButtonBackgroundGreenRoundStyle}" 
                      forms:Message.Attach="Settings" 
                      VerticalOptions="Center" /> 
</Grid>