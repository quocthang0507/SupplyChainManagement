namespace ApplicationCore.Entities
{
    ///<summary>
    ///Hệ tọa độ địa lý (Geographic coordinate system)
    ///</summary>
    public class GeoCoordinate
    {
        ///<summary>
        ///Vĩ độ
        ///</summary>
        public double Latitude { get; set; }
        ///<summary>
        ///Kinh độ
        ///</summary>
        public double Longitude { get; set; }

        public GeoCoordinate()
        {
            Latitude = 0;
            Longitude = 0;
        }

        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}