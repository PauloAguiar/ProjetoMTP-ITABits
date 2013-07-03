using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Nuclex.Graphics.SpecialEffects.Particles;
using Nuclex.Graphics.SpecialEffects.Particles.HighLevel;

namespace Nuclex.Graphics.SpecialEffects.Demo {

  /// <summary>
  ///   Demonstrates the capabilities of the Nuclex.SpecialEffects library
  /// </summary>
  public class SpecialEffectsDemoGame : Microsoft.Xna.Framework.Game {

    /// <summary>Initialiezs a new game</summary>
    public SpecialEffectsDemoGame() {
      this.graphics = new GraphicsDeviceManager(this);

      Content.RootDirectory = "Content";
    }

    /// <summary>
    ///   Allows the game to perform any initialization it needs to before starting
    ///   to run. This is where it can query for any required services and load any
    ///   non-graphics related content. Calling base.Initialize will enumerate through
    ///   any components and initialize them as well.
    /// </summary>
    protected override void Initialize() {

      Components.Add(new FpsComponent(this.graphics));

      // Initialize any registered game components          
      base.Initialize();

      // Set up a camera through which the scene can be viewed
      this.camera = new Camera(
        Matrix.CreateLookAt(
          new Vector3(0.0f, 1.5f, 10.0f), // camera location
          new Vector3(0.0f, 0.0f, 0.0f), // camera focal point
          Vector3.Up // up vector for the camera's orientation
        ),
        Matrix.CreatePerspectiveFieldOfView(
          MathHelper.PiOver4, // field of view
          (float)Window.ClientBounds.Width / (float)Window.ClientBounds.Height, // aspect ratio
          0.01f, 1000.0f // near and far clipping plane
        )
      );

      this.lucidaFont = Content.Load<SpriteFont>("Lucida");

    }
    
    /// <summary>Will be called when your game shuts down</summary>
    /// <param name="calledByUser">
    ///   Whether the shutdown was initiated from user code (as opposed to the GC)
    /// </param>
    /// <remarks>
    ///   If the dispose was initiated by user, any referenced objects we own are
    ///   guaranteed to be alive and can be disposed as well. If the dispose comes
    ///   from the garbage collection, we mustn't access any other objects. We should
    ///   release unmanaged resources in both cases, however.
    /// </remarks>
    protected override void Dispose(bool calledByUser) {
      if(calledByUser) {
        if(this.particleManager != null) {
          this.particleManager.Dispose();
          this.particleManager = null;
        }
      }

      base.Dispose(calledByUser);
    }

    /// <summary>
    ///   LoadContent will be called once per game and is the place to load
    ///   all of your content.
    /// </summary>
    protected override void LoadContent() {

      // Create a new SpriteBatch, which can be used to draw textures.
      this.spriteBatch = new SpriteBatch(GraphicsDevice);

      // Load the effect we'll be using to draw the flare particles
      this.flareTexture = Content.Load<Texture>("FlareTexture");
      this.flareEffect = Content.Load<Effect>("FlareEffect");
      this.flareEffect.Parameters["FlareTexture"].SetValue(this.flareTexture);

      // Create a particle system manager. This is optional and you can manage your
      // particle system yourself if you wish. This manager just makes it easier for
      // us to manage everything. It will automatically manage batched rendering, take
      // care of the vertex declaration and clean everything up when it's destroyed.
      this.particleManager = new ParticleSystemManager(this.graphics);

      // Next, we create a particle system. A particle system is a lightweight
      // container that holds up to a fixed number of particles (in the interest of
      // garbage avoidal) and remembers which affectors (eg. wind, gravity, decay)
      // should be applied. Having several particle systems is quite okay.
      this.flareParticleSystem = new ParticleSystem<FlareParticle>(64000);

      // Add some affectors to the particle system. Affectors modify particles over
      // time. For example, the gravity affector accellerates particles downwards
      // to simulate gravity. The movement affector updates a particle's position
      // by its current velocity. And the decay affector dims the particle slowly.
      this.flareParticleSystem.Affectors.Add(
        new GravityAffector<FlareParticle>(FlareParticleModifier.Default)
      );
      this.flareParticleSystem.Affectors.Add(new DragAffector());
      this.flareParticleSystem.Affectors.Add(
        new MovementAffector<FlareParticle>(FlareParticleModifier.Default)
      );
      this.flareParticleSystem.Affectors.Add(FlareDecayAffector.Default);

#if !XNA_4 // TODO: Write a triangle billboard renderer
      // Add the particle system to the manager. This will auto-create a vertex
      // declaration for it, set up a primitive batch for batched rendering and
      // dispose it upon shutdown. We also specify a pruning delegate that will be
      // used to detect dead particles that can be cleaned up.
      this.particleManager.AddParticleSystem(
        this.flareParticleSystem, // particle system we're adding
        isParticleAlive, // used to detect dead particles
        this.flareEffect // effect used to render particles
      );
#endif
    }

    /// <summary>
    ///   UnloadContent will be called once per game and is the place to unload
    ///   all content.
    /// </summary>
    protected override void UnloadContent() {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    ///   Allows the game to run logic such as updating the world,
    ///   checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {

      // Allows the game to exit
      if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.Exit();

      // Add some new particles to the system
      for(int i = 0; i < 60; ++i) {
        this.flareParticleSystem.AddParticle(
          new FlareParticle(
            Vector3.Zero,
            makeRandomVector() * 10.0f,
            makeRandomColor()
          )
        );
      }

      // Let the particle system manager update all particle systems. This means
      // their registered affectors are applied to the particles. This is done
      // asynchronously and distributed over multiple threads - all you need to
      // know is that your game can do other things while this is happening as
      // long as it's not touching the particle systems in any way!
      IAsyncResult asyncResult = this.particleManager.BeginUpdate(
        1, // Number of update cycled to run
        2, // Number of threads to use simultaneously
        null, null // Callback and state (see .NET asynchronous pattern)
      );

      // TODO: Do other stuff here while the particle system is updating!
      this.camera.HandleControls(gameTime);

      // Complete the asynchronous update. If the update was still running, this
      // line will wait until the update is complete. If one of your evil,
      // badly-written affectors coughed up an exception, it will surface here :)
      this.particleManager.EndUpdate(asyncResult);

      // Perform dead particle elimination. If you're getting nowhere close to
      // the particle limit, you can do also do this asynchronously in the Draw()
      // method while drawing of everything but the particles takes place. Dead
      // particles have to be eliminated in a second step to allow the affectors
      // to be run in multiple threads.
      this.particleManager.Prune();

      // Update other game components
      base.Update(gameTime);

    }

    /// <summary>Called when the game should draw itself.</summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {

      //GraphicsDevice.Clear(Color.CornflowerBlue);
      GraphicsDevice.Clear(Color.Black);

      // Draws all particle systems registered to the particle manager
      this.flareEffect.Parameters["WorldViewProjection"].SetValue(
        this.camera.View * this.camera.Projection
      );
      this.particleManager.Draw(gameTime);

      base.Draw(gameTime);

#if XNA_4
      this.spriteBatch.Begin();
#else
      this.spriteBatch.Begin(
        SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.SaveState
      );
#endif
      try {
        string particlesText = string.Format(
          "Particle Count: {0}", this.flareParticleSystem.Particles.Count
        );

        this.spriteBatch.DrawString(
          this.lucidaFont, particlesText, new Vector2(10.0f, 30.0f), Color.Red
        );
      }
      finally {
        this.spriteBatch.End();
      }

    }

    /// <summary>Determines whether a flare particle is still alive</summary>
    /// <param name="particle">Particle that will be checked</param>
    /// <returns>True if the particle is still alive</returns>
    private static bool isParticleAlive(ref FlareParticle particle) {
      return (particle.Power > 0.1f);
    }

    /// <summary>Creates a vector with random values</summary>
    /// <returns>A vector with random values</returns>
    private Vector3 makeRandomVector() {
      return new Vector3(
        (float)randomNumberGenerator.NextDouble() * 2.0f - 1.0f,
        (float)randomNumberGenerator.NextDouble() * 2.0f - 1.0f,
        (float)randomNumberGenerator.NextDouble() * 2.0f - 1.0f
      );
    }
    
    
    /// <summary>Creates a color with random values</summary>
    /// <returns>A color with random values</returns>
    private Color makeRandomColor() {
      return new Color(
        (float)randomNumberGenerator.NextDouble(),
        (float)randomNumberGenerator.NextDouble(),
        (float)randomNumberGenerator.NextDouble(),
        1.0f
      );
    }

    /// <summary>The random number generator we use</summary>
    private Random randomNumberGenerator = new Random();
    /// <summary>Texture for the flare</summary>
    private Texture flareTexture;
    /// <summary>Camera through which the scene is being viewed</summary>
    private Camera camera;
    /// <summary>Manages our graphics device</summary>
    private GraphicsDeviceManager graphics;
    /// <summary>Batches simple 2D rendering commands</summary>
    private SpriteBatch spriteBatch;
    /// <summary>Manages the particle systems used in the demonstration</summary>
    private ParticleSystemManager particleManager;
    /// <summary>Particle system for a fireworks display</summary>
    private ParticleSystem<FlareParticle> flareParticleSystem;
    /// <summary>Effect used to render flare particles</summary>
    private Effect flareEffect;
    /// <summary>Font used to render overlays</summary>
    private SpriteFont lucidaFont;

  }

} // namespace Nuclex.Graphics.SpecialEffects.Demo
