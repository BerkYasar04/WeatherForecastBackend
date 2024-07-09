using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models.MGM
{
    public class TDistrict
    {
        public object alternatifHadiseIstNo { get; set; }
        public double boylam { get; set; }
        public double enlem { get; set; }
        public int gunlukTahminIstNo { get; set; }
        public string il { get; set; }
        public int ilPlaka { get; set; }
        public string ilce { get; set; }
        public int merkezId { get; set; }
        public int oncelik { get; set; }
        public int? saatlikTahminIstNo { get; set; }
        public int sondurumIstNo { get; set; }
        public int yukseklik { get; set; }
        public string aciklama { get; set; }
        public int modelId { get; set; }
        public int gps { get; set; }
    }
}
