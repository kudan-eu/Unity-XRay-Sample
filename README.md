# Kudan Unity X-Ray Sample

The Unity X-Ray Sample uses a stencil buffer, along with some alpha transparency, to achieve occlusion. The buffer makes a portal effect, making the contents of the box only visible through its lid. Place the marker flat on top of a box and give it a try. This is only used with marker tracking in our sample.

The “Hidden” shader is the one that should be applied to any object you don’t want to view, except through a specific object. The “Portal” object should be applied to the object you want to view hidden objects through.

In order to have the project compile, you will need to download the Kudan Unity Plugin from [the Kudan website](https://www.kudan.eu).