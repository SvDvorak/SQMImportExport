namespace SQMImportExport.Common
{
    public class Vector
    {
        protected bool Equals(Vector other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override int GetHashCode()
        {
            var hashCode = X.GetHashCode();
            hashCode = (hashCode*397) ^ Y.GetHashCode();
            hashCode = (hashCode*397) ^ Z.GetHashCode();
            return hashCode;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Vector) obj);
        }
    }
}