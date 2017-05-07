using System.Collections.Generic;

namespace ConfiguratorTest
{
    public class TestContext
    {
        public class TestContextInner
        {
            public TestContextInner() { }
            public TestContextInner(string _test) { test = _test; }
            public TestContextInner(TestContextInner _test) { test = _test.test; }
            public string test;

            public static TestContextInner GetInstance() {
                if (s_instance1 == null)
                    s_instance1 = new TestContextInner("instance");
                return s_instance1;
            }
            public static TestContextInner GetInstance(string param1) {
                if (s_instance2 == null)
                    s_instance2 = new TestContextInner(param1);
                return s_instance2;
            }
            private static TestContextInner s_instance1 = null;
            private static TestContextInner s_instance2 = null;
        }

        public class TestContextInnerInherited: TestContextInner
        {
            public string inherited;
        }

        public struct TestContextInnerStruct
        {
            public int foo;
        }

        public string string_public;
        private int int_setter;
        private float float_property;
        private string string_nullable = "not_null";
        private bool bool_fromparent = false;
        private string string_fromparentoverwrite;
        public TestContextInner reference_single0 = null;
        public TestContextInner reference_single1 = null;
        public TestContextInner reference_proto0 = null;
        public TestContextInner reference_proto1 = null;
        public TestContextInner nested = null;
        public Dictionary<int, string> map_merge = null;
        public Dictionary<int, string> map = null;
        public List<double> list_merge = null;
        public List<double> list = null;
        public long[] array = null;
        public HashSet<short> set_merge = null;
        public HashSet<short> set = null;
        public Dictionary<string, TestContextInnerStruct> map_struct = null;

        public void setPrivateint(int value) {
            int_setter = value;
        }

        public float FloatProperty {
            get { return float_property; }
            set { float_property = value; }
        }

        public void setNullable(string value) {
            string_nullable = value;
        }

        public void setFromparent(bool value) {
            bool_fromparent = value;
        }

        public void setFromparentoverwrite(string value) {
            string_fromparentoverwrite = value;
        }
    }
}
