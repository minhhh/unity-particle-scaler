# unity-particle-scaler

One of the common problems when working with Unity Shuriken system is that it does not support scaling particles out of the box. When you change the scale of the parent, the children particles do not scale. Even if you somehow get them to scale using scaling mode `Hierarchy`, the end result still does not look good because the velocity of the particles does not scale.

This repo contains an Editor script called `ShurikenScaler` which can help you scale Shuriken particles easily. It will provide an Editor Window that can be accessed via `Window > ShurikenScaler`.

![example 1](/imgs/example1.png)

You can use this Editor Window when you have an GameObject with a `ParticleSystem` component in itself or one of its children. First, select the GameObject in the scene. Then you can change the scale from 0.1 to 100 times the original. The scale will be applied recursively.

You can click `Reset` to set the scale back to 1. You can also select other GameObject and it will remember the scale of the previous ones. Internally, it uses a static array to store the scale by InstanceId. You can also Play, Pause or Stop the particles.

Does not support run-time scaling.

## TODO
* Perhaps add some options to control the scale of individual system such as `ShapeModule`, `VelocityModule`, `ForceModule`, etc?

## Change History

**Version 1.0.1**

* Add ShapeModule scale

**Version 1.0.0**

* Initial commit
