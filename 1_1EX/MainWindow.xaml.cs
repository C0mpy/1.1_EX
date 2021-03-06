﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using _1_1EX.Model;
using Microsoft.Win32;
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace _1_1EX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //precice
        public static RoutedCommand ResetMapFilter = new RoutedCommand();
        public static RoutedCommand SwitchMap1 = new RoutedCommand();
        public static RoutedCommand SwitchMap2 = new RoutedCommand();
        public static RoutedCommand SwitchMap3 = new RoutedCommand();
        public static RoutedCommand SwitchMap4 = new RoutedCommand();

        public static RoutedCommand Exp_res = new RoutedCommand();
        public static RoutedCommand Exp_add = new RoutedCommand();
        public static RoutedCommand Exp_filt = new RoutedCommand();
        public static RoutedCommand Exp_view = new RoutedCommand();
        public static RoutedCommand Exp_sec= new RoutedCommand();

        public static RoutedCommand Type_mngr = new RoutedCommand();
        public static RoutedCommand Tag_mngr = new RoutedCommand();

        Canvas c;

        public static SetPassword sp;

        //za drag
        Image drag_image=null;
        
        Resurs drag_res = null;
        Point drag_from = new Point(-1,-1);
        //

        //ako cemo imati vise mapa da ovde stoji ime aktivne mape?
        public static string active_map;

        public static Dictionary<string, List<MapModel>> map_model = new Dictionary<string, List<MapModel>>();

        //i mozda da cuvamo resurse za svaku mapu u hashmap gde je kljuc ime mape?
        public static List<Resurs> resursi = new List<Resurs>();

        public static ObservableCollection<TipResursa> types
        {
            get;
            set;
        }

        public static ObservableCollection<Etiketa> tags
        {
            get;
            set;
        }

        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = this;
            resurs = new Resurs();
            active_map = "map1";
            types = Serializer.LoadTip();
            tags = Serializer.LoadEtiketa();
            resursi = Serializer.ReadResources();
            picker.SelectedDate = DateTime.Today;

            //meci u funkciju kasnije da ne bude ruzno :S
            {
                c = mapa;

            }
            frekvencija_q.SelectedIndex = 0; //hack
            box_frekvencija_q.SelectedIndex = 0;
            type_q.Items.Add("All");
            foreach (TipResursa t in types)
            {
                type_q.Items.Add(t.Ime);
                box_type_q.Items.Add(t.Ime);
            }
            type_q.SelectedIndex = 0;
            box_type_q.SelectedIndex = 0;
            map_model["map1"] = new List<MapModel>();
            map_model["map2"] = new List<MapModel>();
            map_model["map3"] = new List<MapModel>();
            map_model["map4"] = new List<MapModel>();
            ucitajResurse(resursi);
            fillBoxFilterTags();
            loadMapContent();
            dodaj_precice();
        }

        private void dodaj_precice()
        {

            //resetuj filter za mapu
            ResetMapFilter.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(ResetMapFilter, Reset_Map_Filter));

            //menjanje mapa
            SwitchMap1.InputGestures.Add(new KeyGesture(Key.D1, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SwitchMap1,nest1));
            SwitchMap2.InputGestures.Add(new KeyGesture(Key.D2, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SwitchMap2, nest2));
            SwitchMap3.InputGestures.Add(new KeyGesture(Key.D3, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SwitchMap3, nest3));
            SwitchMap4.InputGestures.Add(new KeyGesture(Key.D4, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(SwitchMap4, nest4));

            Exp_res.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(Exp_res, togl1));

            Exp_add.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(Exp_add, togl2));

            Exp_filt.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(Exp_filt, togl3));

            Exp_view.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(Exp_view, togl4));

            Exp_sec.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(Exp_sec, togl5));

            Type_mngr.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(Type_mngr, TypeManager));

            Tag_mngr.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(Tag_mngr, TagManager));


        }

        private void togl1(object sender, EventArgs e)
        {
            if (exp_res.IsExpanded)
            {
                exp_res.IsExpanded = false;
            }
            else
            {
                exp_res.IsExpanded = true;
            }
        }

        private void togl2(object sender, EventArgs e)
        {
            if (exp_add.IsExpanded)
            {
                exp_add.IsExpanded = false;
            }
            else
            {
                exp_add.IsExpanded = true;
            }
        }


        private void togl3(object sender, EventArgs e)
        {
            if (exp_filt.IsExpanded)
            {
                exp_filt.IsExpanded = false;
            }
            else
            {
                exp_filt.IsExpanded = true;
            }
        }

        private void togl4(object sender, EventArgs e)
        {
            if (exp_view.IsExpanded)
            {
                exp_view.IsExpanded = false;
            }
            else
            {
                exp_view.IsExpanded = true;
            }
        }

        private void togl5(object sender, EventArgs e)
        {
            if (exp_sec.IsExpanded)
            {
                exp_sec.IsExpanded = false;
            }
            else
            {
                exp_sec.IsExpanded = true;
            }
        }
        private void nest1(object sender,EventArgs e)
        {
            mp1.IsChecked = true;

        }
        private void nest2(object sender, EventArgs e)
        {
            mp2.IsChecked = true;

        }
        private void nest3(object sender, EventArgs e)
        {
            mp3.IsChecked = true;
            

        }
        private void nest4(object sender, EventArgs e)
        {
            mp4.IsChecked = true;

        }


        public void ucitajResurse(List<Resurs> res)
        {
            int num_col = 6;
            gr1.Children.Clear();
            gr1.Height = 136;
            int br = res.Count;
            Image[] carImg = new Image[br];

            MenuItem[] mi = new MenuItem[br];
            Border[] bor = new Border[br];


            ColumnDefinition[] gridCol1 = new ColumnDefinition[br / num_col + num_col];
            RowDefinition[] gridRow1 = new RowDefinition[br];

            for (int j = 0; j < num_col; j++)
            {
                gridCol1[j] = new ColumnDefinition();
                gridCol1[j].Width = new GridLength(40);


                gr1.ColumnDefinitions.Add(gridCol1[j]);
            }

            int col = -1;
            for (int i = 0; i < br; i++)
            {

                if ((i % num_col) == 0)
                {
                    col++;
                    gridRow1[i / num_col] = new RowDefinition();
                    //gridRow1[i].Height = new GridLength(40);

                    gridRow1[i / num_col].Height = new GridLength(0.5, GridUnitType.Star);
                    gridRow1[i / num_col].Height = GridLength.Auto;
                    gr1.RowDefinitions.Add(gridRow1[i / num_col]);


                }

                
                if (col > 2 & ((i % num_col) == 0))
                {
                    gr1.Height += 40;
                }

                carImg[i] = new Image();
                carImg[i].Source = new BitmapImage(new Uri(res[i].Ikonica, UriKind.RelativeOrAbsolute)); ;

                             
                carImg[i].ToolTip = createToolTip(i);
                ToolTipService.SetShowDuration(carImg[i], 20000);

               
                int index = i;
                    MenuItem modMenuItem = new MenuItem();
                    modMenuItem.Header = "Modify";
                    modMenuItem.Click += (sender, e) => modifyResourceAction(index);
                    MenuItem delMenuItem = new MenuItem();
                    delMenuItem.Header = "Delete";
                    delMenuItem.Click += (sender, e) => deleteResourceAction(index);
                    ContextMenu cm = new ContextMenu();
                    cm.Items.Add(modMenuItem);
                    cm.Items.Add(delMenuItem);
                    carImg[i].ContextMenu = cm;
                    carImg[i].MouseLeftButtonDown += (sender, e) => startDrag(carImg[index],res[index]);
                

                carImg[i].Width = 40;
                carImg[i].Height = 40;

                RenderOptions.SetBitmapScalingMode(carImg[i], BitmapScalingMode.Fant);
                bor[i] = new Border();
                bor[i].Child = carImg[i];
                bor[i].Visibility = System.Windows.Visibility.Visible;
                bor[i].BorderBrush = Brushes.Silver;
                bor[i].BorderThickness = new Thickness(1);
                bor[i].SetValue(Grid.ColumnProperty, i % num_col);
                bor[i].SetValue(Grid.RowProperty, col);

                gr1.Children.Add(bor[i]);



            }

        }

        private object createToolTip(int i)
        {
            // Set up the ToolTip text for image

                String etikete = "";
                for (int k = 0; k < resursi[i].Etikete1.Count; k++) {
                    etikete += "[" + resursi[i].Etikete1[k].Id + "] ";
                }
                if (etikete == "")
                    etikete = "Doesn't have tags";

                String tip = "";
                if (resursi[i].Tip.Id == "")
                    tip = "Doesn't have type";
                else
                    tip = "[" + resursi[i].Tip.Id + "]";

                String pom = resursi[i].Opis;
                String opis = "";
                bool usao = false;

                if (pom.Length <= 25)
                    opis = pom;
                
                while (pom.Length > 25) {
                    String dodatak = "";
                    if (pom[24] == ' ' || (pom[23] == ' ' && pom[25] == ' '))
                        dodatak = "\n\t      ";
                    else
                        dodatak = "-\n\t      ";
                    opis += pom.Substring(0, 25)+dodatak;
                    pom = pom.Substring(25);
                    usao = true;
                }
                if (usao)
                    opis += pom;

                if (opis == "")
                    opis = "Doesn't have description";

            ToolTip tooltip = new ToolTip { Content = "Id: "+resursi[i].Id+"\n"+
                                                          "Name: "+resursi[i].Ime+"\n"+
                                                          "Type Id: "+tip+"\n"+
                                                          "Frequency: "+resursi[i].Frekvencija1+"\n"+
                                                          "Renewable: "+resursi[i].Obnovljiv+"\n"+
                                                          "Strategic: "+resursi[i].Vaznost+"\n"+
                                                          "Exploitable: "+resursi[i].Eksploatacija+"\n"+
                                                          "Measure: "+resursi[i].Mera1+"\n"+
                                                          "Price: "+resursi[i].Cena+"$\n"+
                                                          "Discovery Date: "+resursi[i].Datum+"\n"+
                                                          "Tags: "+etikete+"\n"+
                                                          "Description: "+opis};
            return tooltip;
        }

        public void ucitajEtikete()
        {

            gr2.Children.Clear();
            int br = resurs.Etikete1.Count;
            Rectangle[] carImg = new Rectangle[br];
            MenuItem[] mi = new MenuItem[br];
            Border[] bor = new Border[br];
            ColumnDefinition[] gridCol1 = new ColumnDefinition[3];
            RowDefinition[] gridRow1 = new RowDefinition[br / 3 + 1];

            for (int j = 0; j < 3; j++)
            {
                gridCol1[j] = new ColumnDefinition();
                gridCol1[j].Width = new GridLength(31);
                gr2.ColumnDefinitions.Add(gridCol1[j]);
            }

            int row = -1;
            for (int i = 0; i < br; i++)
            {
                if ((i % 3) == 0)
                {
                    row++;
                    gridRow1[row] = new RowDefinition();
                    gridRow1[row].Height = new GridLength(0.5, GridUnitType.Star);
                    gridRow1[row].Height = GridLength.Auto;
                    gr2.RowDefinitions.Add(gridRow1[row]);

                }

                carImg[i] = new Rectangle();
                carImg[i].Height = 25;
                carImg[i].Width = 31;
                carImg[i].Fill = new SolidColorBrush(resurs.Etikete1[i].Boja);

                RenderOptions.SetBitmapScalingMode(carImg[i], BitmapScalingMode.Fant);
                bor[i] = new Border();
                bor[i].Child = carImg[i];
                bor[i].Visibility = System.Windows.Visibility.Visible;
                bor[i].BorderBrush = Brushes.Silver;
                bor[i].BorderThickness = new Thickness(1);
                bor[i].SetValue(Grid.ColumnProperty, i % 3);
                bor[i].SetValue(Grid.RowProperty, row);

                gr2.Children.Add(bor[i]);
                
            }
        }


        //pocni drag sa liste resursa
        private void startDrag(Image img,Resurs res)
        {
            bool can_drag = true;
            foreach (MapModel mm in map_model[active_map])
            {
                if (mm.Res.Id.Equals(res.Id))
                    can_drag = false;
            }

            if (can_drag)
            {

                drag_res = res;

                drag_image = new Image();
                drag_image.Source = img.Source;
                drag_image.Height = 30;
                drag_image.Width = 30;

                drag_image.ToolTip = img.ToolTip;
                ToolTipService.SetShowDuration(drag_image, 10000);

                // Initialize the drag & drop operation
                mapa.Children.Add(drag_image);


                DragDrop.DoDragDrop(this, img, DragDropEffects.Move);
            }
            else
            {
                MessageBox.Show("This resource already exists on map.");
            }
        }
        //pocni drag na kanvasu
        private void Canvas_StartDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Image)
            {
                    c.CaptureMouse();
                    Point point = new Point();
                    point = e.GetPosition(mapa);

                    //model for deletion
                    MapModel del = null; 

                    foreach (MapModel m in map_model[active_map])
                    {
                        if ((m.Top < point.Y && m.Top+30>point.Y)&& ( m.Left < point.X && m.Left +30>point.X))
                        {
                            drag_res = m.Res;
                            del = m;
                            drag_from.X = m.Left;
                            drag_from.Y = m.Top;
                        }
                    }

                    map_model[active_map].Remove(del);
                   
                    drag_image = e.OriginalSource as Image;
                    DragDrop.DoDragDrop(this, drag_image, DragDropEffects.Move );
                
            }
        }
        //dropaj na kanvas
        private void Canvas_Drop(object sender, DragEventArgs e)
        {

                Point point = new Point();
                point = e.GetPosition(c);

                //proveri dal se preklapaju
                bool can_drop = true;
                foreach (MapModel m in map_model[active_map])
                {
                    if ((m.Top < point.Y && m.Top + 30 > point.Y) && (m.Left < point.X && m.Left + 30 > point.X))
                    {
                        can_drop = false;
                    }
                }

               //za dregovanje sa mape
                if (!can_drop)
                    point = drag_from;
                //za dregovanje sa grida
                if (point.X == -1)
                    point=new Point(0,0);
                
                    Canvas.SetLeft(drag_image, point.X);
                    Canvas.SetTop(drag_image, point.Y);

                    //save map content
                    map_model[active_map].Add(new MapModel( point.Y, point.X, drag_res));
                    Serializer.SaveMapModel();

                //reset
                drag_image = null;
                
                drag_res= null;
                drag_from.X = -1;
                drag_from.Y = -1;
      
        }

        private void Canvas_DragOver(object sender, DragEventArgs e)
        {
           
            Point point = new Point();
            point = e.GetPosition(mapa);
            Canvas.SetLeft(drag_image, point.X);
            Canvas.SetTop(drag_image, point.Y);

        }


        private void Map_Filter(object sender, EventArgs e)
        {
            //filtrirani mapmodel
            List<MapModel> filter_result = new List<MapModel>();


            //pokupi sve uslove/////////////////////////
            string name_query = map_filter_text.Text;


            bool obnovljivv = (bool)obnovljiv_q.IsChecked;
            bool vazno = (bool)vaznost_q.IsChecked;
            bool eksploat = (bool)eksploatacija_q.IsChecked;
           
            string   freq = ((ComboBoxItem)frekvencija_q.SelectedItem).Content.ToString();
            string ty = "All";
            try
            {
                ty =(string) type_q.SelectedItem;
            }
            catch (Exception ee)
            {
                //nista jbg :/
            }
            //////////////////////////////////////////


            c.Children.Clear();
            
            //filter//sve u jednoj iteraciji?
            try
            {
                foreach (MapModel mm in map_model[active_map])
                {
                    bool ok = true;

                    //name filter
                    if (!mm.Res.Ime.ToLower().Contains(name_query.ToLower()))
                        ok = false;

                    //ove filter
                    if (obnovljivv)
                        if (!mm.Res.Obnovljiv)
                            ok = false;
                    if (vazno)
                        if (!mm.Res.Vaznost)
                            ok = false;
                    if (eksploat)
                        if (!mm.Res.Eksploatacija)
                            ok = false;

                    //frekvencija filter
                    if (freq != "All")
                        if (mm.Res.Frekvencija1.ToString() != freq)
                            ok = false;

                    //tip filter
                    if (ty != "All")
                        if (mm.Res.Tip.Ime != ty)
                            ok = false;

                    if (ok)
                        filter_result.Add(mm);
                }
            }
            catch (Exception eee)
            {
            }

            foreach (MapModel fmm in filter_result)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(fmm.Res.Ikonica, UriKind.RelativeOrAbsolute));
                img.Height = 30;
                img.Width = 30;
                Canvas.SetLeft(img, fmm.Left);
                Canvas.SetTop(img, fmm.Top);
                mapa.Children.Add(img);
            }

        }

        private void Reset_Map_Filter(object sender, EventArgs e)
        {
            map_filter_text.Text = "";
            obnovljiv_q.IsChecked = false;
            vaznost_q.IsChecked = false;
            eksploatacija_q.IsChecked = false;
            frekvencija_q.SelectedIndex = 0;
            type_q.SelectedIndex = 0;
            loadMapContent();
        }

        private void Box_Filter(object sender, EventArgs e)
        {
            //filtrirani mapmodel
            List<Resurs> filter_result = new List<Resurs>();
            //pokupi sve uslove
            string name_query = box_filter_text.Text;

            bool obnovljivv = (bool)box_obnovljiv_q.IsChecked;
            bool vazno = (bool)box_vaznost_q.IsChecked;
            bool eksploat = (bool)box_eksploatacija_q.IsChecked;

            string freq = ((ComboBoxItem)box_frekvencija_q.SelectedItem).Content.ToString();
            string ty = "All";
            try
            {
                ty = (string)box_type_q.SelectedItem;
            }
            catch (Exception ee)
            {
                //nista jbg :/
            }
            try
            {
                foreach (Resurs r in resursi)
                {
                    bool ok = true;
                    if (!r.Ime.ToLower().Contains(name_query.ToLower()))
                        ok = false;
                    //ove filter
                    if (obnovljivv)
                    {
                        if (!r.Obnovljiv)
                            ok = false;
                    }
                    else
                    {
                        if(r.Obnovljiv)
                            ok = false;
                    }
                    if (vazno)
                    {
                        if (!r.Vaznost)
                            ok = false;
                    }
                    else 
                    {
                        if (r.Vaznost)
                            ok = false;
                    }
                    if (eksploat)
                    {
                        if (!r.Eksploatacija)
                            ok = false;
                    }
                    else {
                        if(r.Eksploatacija)
                            ok = false;
                    }
                    for (int i = 0; i < FindVisualChildren<CheckBox>(gr4).ToList().Count; i++)
                    {
                        CheckBox cb = FindVisualChildren<CheckBox>(gr4).ElementAt(i);
                        if (cb.IsChecked == true)
                        {
                            bool hazTag = false;
                            for (int j = 0; j < r.Etikete1.Count; j++)
                            {
                                if (tags.ElementAt(i).Id == r.Etikete1.ElementAt(j).Id)
                                {
                                    hazTag = true;
                                }
                            }
                            if (!hazTag)
                            {
                                ok = false;
                                break;
                            }
                        }
                    }
                    //frekvencija filter
                    if (freq != "All")
                        if (r.Frekvencija1.ToString() != freq)
                            ok = false;

                    //tip filter
                    if (ty != "All")
                        if (r.Tip.Ime != ty)
                            ok = false;

                    if (ok)
                        filter_result.Add(r);
                }
            }
            catch (Exception eee)
            {
            }
            ucitajResurse(filter_result);
            

        }

        private void loadMapContent()
        {

            map_model[active_map]=Serializer.LoadMapModel();

            c.Children.Clear();

            foreach (MapModel e in map_model[active_map])
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(e.Res.Ikonica, UriKind.RelativeOrAbsolute));
                img.Height = 30;
                img.Width = 30;
                int index = FindIndexResource(e.Res.Id);
                img.ToolTip = createToolTip(index);
                ToolTipService.SetShowDuration(img, 20000);
                Canvas.SetLeft(img,e.Left);
                Canvas.SetTop(img, e.Top);
                c.Children.Add(img);
            }

        }


        private int FindIndexResource(string id)
        {
            for (int i = 0; i < resursi.Count; i++)
                if (resursi[i].Id == id)
                    return i;
            return -1;
        }

        private void Switch_Active(object sender,EventArgs e)
        {
            Reset_Map_Filter(null, null);
            active_map = (string)((RadioButton)sender).Content;
            loadMapContent();
        }

       

        private void modifyResourceAction(int i)
        {
            WinResurs wr = new WinResurs(this, resursi[i]);
            wr.ShowDialog();
            ucitajResurse(resursi);
        }

        private void deleteResourceAction(int i)
        {
            MessageBox.Show("Resource with id " + resursi[i].Id + " has been deleted!");
            resursi.RemoveAt(i);
            ucitajResurse(resursi);
            Serializer.WriteResources();
        }

        private void odaberiIkonicu(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
            };
            darkwindow.Show();
            Nullable<bool> result = openFileDialog.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = openFileDialog.FileName;
                //string startupPath = Environment.CurrentDirectory;
                //System.IO.File.Copy(filename, startupPath.Substring(0, startupPath.Length - 9) + "SlikeResursi\\" + System.IO.Path.GetFileName(filename));
                resurs.Ikonica = filename;

                iconDisplay.Source = new BitmapImage(new Uri(filename));
                removeButton.Visibility = Visibility.Visible;
            }
            darkwindow.Close();
        }

        private void DatePicker_SelectedDateChanged(object sender,
        SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            
        }

        #region NotifyProperties
        public static Resurs resurs;
        public TipResursa Type
        {
            get
            {
                return resurs.Tip;
            }
            set
            {
                if (value != resurs.Tip)
                {
                    resurs.Tip = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public string Id
        {
            get
            {
                return resurs.Id;
            }
            set
            {
                if (value != resurs.Id)
                {
                    resurs.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Cena
        {
            get
            {
                return resurs.Cena;
            }
            set
            {
                if (value != resurs.Cena)
                {
                    resurs.Cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }

        public string Ime
        {
            get
            {
                return resurs.Ime;
            }
            set
            {
                if (value != resurs.Ime)
                {
                    resurs.Ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }
        #endregion

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        void dodajResursFormReset()
        {
            id.Text = "";
            ime.Text = "";
            opis.Text = "";
            frekvencija.SelectedIndex = 0;
            obnovljiv.IsChecked = false;
            vaznost.IsChecked = false;
            eksploatacija.IsChecked = false;
            mera.SelectedIndex = 0;
            cena.Text = "0";
            picker.SelectedDate = DateTime.Today;
            gr2.Children.Clear();
            removeButton.Visibility = Visibility.Collapsed;
            resurs.Ikonica = "";
            iconDisplay.Source = null;
            typeIcon.Source = null;

        }

        void dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (resurs.Id == "" || resurs.Ime == "")
            {
                id.Text = "1";
                id.Text = "";
                ime.Text = "1";
                ime.Text = "";
                return;
            }
            
            resurs.Ime = ime.Text;
            resurs.Opis = opis.Text;
            resurs.Frekvencija1 = (Frekvencija)Enum.Parse(typeof(Frekvencija), frekvencija.Text);
            resurs.Obnovljiv = (bool)obnovljiv.IsChecked;
            resurs.Vaznost = (bool)vaznost.IsChecked;
            resurs.Eksploatacija = (bool)eksploatacija.IsChecked;
            resurs.Mera1 = (Mera)Enum.Parse(typeof(Mera), mera.Text);
            resurs.Datum = (DateTime)picker.SelectedDate;
            if (resurs.Ikonica == "") {
                string startupPath = Environment.CurrentDirectory;
                resurs.Ikonica = startupPath.Substring(0, startupPath.Length - 9) + "/resources.png";
            }
                
            resursi.Add(resurs);

            ucitajResurse(resursi);
            Serializer.WriteResources();
            resurs = new Resurs();
            dodajResursFormReset();

        }
        private void skiniSifru(object sender, RoutedEventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\pass.txt";
            File.Create(path).Close();
            MessageBox.Show("Now the password is not required to run the program");
        }

        private void dodajSifru(object sender, RoutedEventArgs e)
        {
            sp = new SetPassword();
            sp.Show();
        }

        private void TypeSelect(object sender, RoutedEventArgs e)
        {
            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
            };
            darkwindow.Show();
            var ew = new _1_1EX.WinTip.TypeSelect(types, resurs);
            ew.ShowDialog();
            darkwindow.Close();
            MessageBox.Show(resurs.ToString());
            if (resurs.Tip.Ikonica != "")
            {
                typeIcon.Source = new BitmapImage(new Uri(resurs.Tip.Ikonica));
            }
        }

        private void Box_Filter_Reset(object sender, RoutedEventArgs e)
        {
            box_filter_text.Text = "";
            box_obnovljiv_q.IsChecked = false;
            box_vaznost_q.IsChecked = false;
            box_eksploatacija_q.IsChecked = false;
            box_frekvencija_q.SelectedIndex = 0;
            box_type_q.SelectedIndex = 0;
            ucitajResurse(resursi);
        }

        private void TypeManager(object sender, RoutedEventArgs e)
        {
            var ew = new _1_1EX.WinTip.TypeManagement(types);

            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
            };
            darkwindow.Show();
            ew.ShowDialog();
            darkwindow.Close();
        }

        private void TagManager(object sender, RoutedEventArgs e)
        {
            var ew = new _1_1EX.WinEtiketa.TagManagement(tags);

            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
            };
            darkwindow.Show();
            ew.ShowDialog();
            darkwindow.Close();
            fillBoxFilterTags();
        }

        private void TagSelect(object sender, RoutedEventArgs e)
        {
            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
            };
            darkwindow.Show();
            var ew = new _1_1EX.WinEtiketa.TagSelect(tags, resurs);
            ew.ShowDialog();
            darkwindow.Close();
            ucitajEtikete();
        }

        private void removePath(object sender, RoutedEventArgs e)
        {
            resurs.Ikonica = "";
            iconDisplay.Source = null;
            removeButton.Visibility = Visibility.Collapsed;
        }

        public void fillBoxFilterTags()
        {
            gr4.Children.Clear();

            for (int i = 0; i < tags.Count(); i++)
            {
                DockPanel sp = new DockPanel();
                sp.HorizontalAlignment = HorizontalAlignment.Left;
                sp.Height = 30;
                sp.Width = 180;
                CheckBox cb = new CheckBox();
                cb.Checked += Box_Filter;
                cb.Unchecked += Box_Filter;
                cb.VerticalAlignment = VerticalAlignment.Center;
                Thickness margin = cb.Margin;
                margin.Right = 10;
                cb.Margin = margin;
                cb.SetValue(DockPanel.DockProperty, Dock.Left);
                Label lb = new Label();
                lb.Height = 30;
                lb.Content = tags[i].Id;
                Label lb2 = new Label();
                lb2.Height = 30;
                lb2.Background = new SolidColorBrush(tags[i].Boja);
                lb2.Width = 30;
                lb2.SetValue(DockPanel.DockProperty, Dock.Right);
                lb.SetValue(DockPanel.DockProperty, Dock.Top);
                sp.Children.Add(cb);
                sp.Children.Add(lb2);
                sp.Children.Add(lb);
                gr4.Children.Add(sp);
            }
        }

        //pa zar mora ovo da se koristi da bih dobio element iz DataGridTemplateColumn u WPF?!
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

    }
}
