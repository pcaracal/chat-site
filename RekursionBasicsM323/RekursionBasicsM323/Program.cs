namespace RekursionBasicsM323 {
  class Program {
    static void EinMalEinsFor() {
      for (int i = 1; i <= 10; i++) {
        for (int y = 1; y <= 10; y++) {
          Console.Write($"{i * y} ");
        }

        Console.Write('\n');
      }
    }

    static string EinMalEinsInnerRecursive(int maxw, int mul, string outstr) {
      if (maxw == 0) return outstr;
      return EinMalEinsInnerRecursive(maxw - 1, mul, $"{maxw * mul} {outstr}");
    }

    static string EinMalEinsOuterRecursive(int maxw, int maxh) {
      if (maxh == 0) return "";
      return $"{EinMalEinsOuterRecursive(maxw, maxh - 1)}\n{EinMalEinsInnerRecursive(maxw, maxh, "")}";
    }

    static void Main(String[] args) {
      // EinMalEinsFor();
      Console.WriteLine(EinMalEinsOuterRecursive(15, 15));
    }
  }
}