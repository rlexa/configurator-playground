using Configurator;
using System;
using System.Collections.Generic;

namespace ConfiguratorTest
{
    class Program
    {
        public const int EXITCODE_OK = 0;
        public const int EXITCODE_PATH = 1;
        public const int EXITCODE_PARSE = 2;
        public const int EXITCODE_TEST = 3;

        public const string PARAM_CONTEXTPATH = "../../context";

        static void Main(string[] args) {
            int iExitCode = 0;

            string[] paths = new string[] { PARAM_CONTEXTPATH + ".xml", PARAM_CONTEXTPATH + ".json" };
            foreach (string path in paths) {
                if (iExitCode == EXITCODE_OK) {
                    try {
                        IContext context = ContextLoader.loadContext(path);

                        try { context.inflate("fail_noparent"); } catch (Exception) { }

                        bool bool_ = (bool)context.inflate("bool");
                        char char_ = (char)context.inflate("char");
                        short short_ = (short)context.inflate("short");
                        int int_ = (int)context.inflate("int");
                        long long_ = (long)context.inflate("long");
                        ushort ushort_ = (ushort)context.inflate("ushort");
                        uint uint_ = (uint)context.inflate("uint");
                        ulong ulong_ = (ulong)context.inflate("ulong");
                        float float_ = (float)context.inflate("float");
                        double double_ = (double)context.inflate("double");
                        string string_ = (string)context.inflate("string");

                        Int16 int16 = (Int16)context.inflate("int16");
                        Int32 int32 = (Int32)context.inflate("int32");
                        Int64 int64 = (Int64)context.inflate("int64");

                        TestContext test = context.inflate("test") as TestContext;
                        bool bSingle = test.reference_single0 == test.reference_single1;
                        bool bProto = test.reference_proto0 == test.reference_proto1;
                        bool bFailed = false;
                        bool bFailedParent = false;
                        try {
                            context.inflate("testParent");
                        } catch (Exception) { bFailedParent = true; }
                        try {
                            TestContext.TestContextInner testImported = context.inflate("testImportedId") as TestContext.TestContextInner;
                        } catch (Exception) { bFailed = true; }
                        try {
                            TestContext.TestContextInner testOverride = context.inflate("testOverride") as TestContext.TestContextInner;
                        } catch (Exception) { bFailed = true; }
                        try {
                            TestContext.TestContextInner testSConstructor1 = context.inflate("testStaticConstructor1") as TestContext.TestContextInner;
                            bFailed |= testSConstructor1.test != "static";
                            TestContext.TestContextInner testSConstructor2 = context.inflate("testStaticConstructor2") as TestContext.TestContextInner;
                            bFailed |= testSConstructor2.test != "static_param";
                        } catch (Exception) { bFailed = true; }
                        try {
                            TestContext.TestContextInner testConstructor = context.inflate("testConstructor") as TestContext.TestContextInner;
                            bFailed |= testConstructor.test != "constructed";
                            TestContext.TestContextInner testCConstructor = context.inflate("testCopyConstructor") as TestContext.TestContextInner;
                            bFailed |= testCConstructor.test != "constructed";
                        } catch (Exception) { bFailed = true; }
                        iExitCode = bFailedParent && !bFailed ? EXITCODE_OK : EXITCODE_TEST;
                    } catch (Exception ex) {
                        iExitCode = EXITCODE_PARSE;
                        Console.Error.WriteLine("ERROR: Exception while parsing '" + path + "': {0}", ex);
                        Console.Error.WriteLine(ex.StackTrace);
                    }
                }
            }

            Environment.Exit(iExitCode);
        }

        private static void testGenerics() {
            Type generic = typeof(Dictionary<,>);
            __DisplayTypeInfo(generic);
            Type[] typeArgs = { typeof(string), typeof(TestContext) };
            Type constructed = generic.MakeGenericType(typeArgs);
            __DisplayTypeInfo(constructed);
            Console.WriteLine("\r\n--- Compare types obtained by different methods:");
            Type t = typeof(Dictionary<String, TestContext>);
            Console.WriteLine("\tAre the constructed types equal? {0}", t == constructed);
            Console.WriteLine("\tAre the generic types equal? {0}",
                t.GetGenericTypeDefinition() == generic);
        }

        private static void __DisplayTypeInfo(Type t) {
            Console.WriteLine("\r\n{0}", t);

            Console.WriteLine("\tIs this a generic type definition? {0}",
                t.IsGenericTypeDefinition);

            Console.WriteLine("\tIs it a generic type? {0}",
                t.IsGenericType);

            Type[] typeArguments = t.GetGenericArguments();
            Console.WriteLine("\tList type arguments ({0}):", typeArguments.Length);
            foreach (Type tParam in typeArguments) {
                Console.WriteLine("\t\t{0}", tParam);
            }
        }
    }
}
