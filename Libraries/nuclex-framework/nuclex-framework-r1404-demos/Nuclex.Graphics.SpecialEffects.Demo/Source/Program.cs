using System;

namespace Nuclex.Graphics.SpecialEffects.Demo {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args) {
      using(SpecialEffectsDemoGame game = new SpecialEffectsDemoGame()) {
        game.Run();
      }
    }
  }
}

