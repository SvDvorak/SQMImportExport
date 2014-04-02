using NUnit.Framework;
using SQMImportExport.Common;

namespace SQMImportExport.Tests
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void Expect_same_values_in_properties_when_creating_a_vector()
        {
            var vector = new Vector(1, 2, 3);

            Assert.AreEqual(1, vector.X);
            Assert.AreEqual(2, vector.Y);
            Assert.AreEqual(3, vector.Z);
        }

        [Test]
        public void Vector_and_other_type_are_not_equal()
        {
            var vector = new Vector(0, 0, 0);

            Assert.AreNotEqual(vector, 3);
        }

        [Test]
        public void Vector_and_null_are_not_equal()
        {
            var vector = new Vector(0, 0, 0);

            Assert.AreNotEqual(vector, null);
        }

        [Test]
        public void Vector_is_equal_itself()
        {
            var vector = new Vector(1, 2, 3);

            Assert.AreEqual(vector, vector);
        }

        [Test]
        public void Vector_is_not_equal_other_vector_when_x_is_different()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(2, 2, 3);

            Assert.AreNotEqual(vector1, vector2);
        }

        [Test]
        public void Vector_is_not_equal_other_vector_when_y_is_different()
        {
            var vector1 = new Vector(1, 5, 3);
            var vector2 = new Vector(2, 2, 3);

            Assert.AreNotEqual(vector1, vector2);
        }

        [Test]
        public void Vector_is_not_equal_other_vector_when_z_is_different()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 4);

            Assert.AreNotEqual(vector1, vector2);
        }

        [Test]
        public void Vector_is_equal_other_vector_when_all_components_are_same()
        {
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);

            Assert.AreEqual(vector1, vector2);
        }
    }
}
