﻿using System;
using DPSF_Demo.ParticleSystems;
using Microsoft.Xna.Framework;

namespace DPSF_Demo.Particle_System_Wrappers_For_DPSF_Demo
{
#if (WINDOWS)
	[Serializable]
#endif
	class DefaultSprite3DBillboardDPSFDemoParticleSystemTemplateWrapper : DefaultSprite3DBillboardParticleSystemTemplate, IWrapDPSFDemoParticleSystems
	{
        public DefaultSprite3DBillboardDPSFDemoParticleSystemTemplateWrapper(Game cGame)
            : base(cGame)
        { }

        public void AfterAutoInitialize()
        { }

	    public void DrawStatusText(DrawTextRequirements draw)
	    { }

	    public void DrawInputControlsText(DrawTextRequirements draw)
	    { }

	    public void ProcessInput()
	    { }
	}
}