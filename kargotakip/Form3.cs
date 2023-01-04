using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace kargotakip
{
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        public int DELIVERED;
        public int UNDELIVERED;
        public int DISTRIBUTION;
        public int INCENTER;
        public int TRANSFER;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Func<ChartPoint, string> labelPiont = charpoint => String.Format("{0} ({1:p})", charpoint.Y, charpoint.Participation);

            // TODO: Bu kod satırı 'kargoDataSet.kargolar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            pieChart1.LegendLocation = LegendLocation.Right;
            LiveCharts.SeriesCollection series = new LiveCharts.SeriesCollection();

            PieSeries pie1 = new PieSeries();
            pie1.Title = "DELIVERED";
            pie1.Values = new ChartValues<int> { DELIVERED };
            pie1.DataLabels = true;
             pie1.LabelPoint = labelPiont;
            series.Add(pie1);
            PieSeries pie2 = new PieSeries();
            pie2.Title = "UNDELIVERED";
            pie2.Values = new ChartValues<int> { UNDELIVERED };
            pie2.DataLabels = true;
             pie2.LabelPoint = labelPiont;
            series.Add(pie2);
          






            pieChart1.Series = series;


        }
    }
}
