<h2>News</h2>

01/07/2016: <b>Busy Indicator - MS Productivity Future Vision Style</b>

After learning about the great library from <a href="https://github.com/ratishphilip">ratishphilip</a>, <a href="https://github.com/ratishphilip/CompositionExpressionToolkit">CompositionExpressionToolkit</a> now it is possible to create custom shapes and animations just using the Visual Layer.

<img src="https://github.com/juanpaexpedite/MasterDetailsComposition/blob/master/busyindicator_capture.jpg"/>

In this case I created an outlined ellipsed masks with the exclusion of two ellipses, then I create four of them and I create several animations in order to add into a Busy Indicator control that animates really smooth using Composition. You can play with it in the AnimatedControls project.

<h2>MasterDetailsComposition</h2>

Single Page Master Details Full Example using compositon

<img src="https://github.com/juanpaexpedite/MasterDetailsComposition/blob/master/MasterDetailsComposition/readme0.jpg"/>

<img src="https://github.com/juanpaexpedite/MasterDetailsComposition/blob/master/MasterDetailsComposition/readme.jpg"/>

<h1>Introduction</h1>

In this example I have created the following structure

<h2>Attached Properties</h2>
To keep XAML simple I created several attached properties that when are attached, they activate the composition layer:

<ul>
<li>EffectComposition: Is a dummy/tag property to save the Composition Effect using in a UIElement.</li>
<li>BlendForeground: Activates the effect in a Panel to change a background with the arithmetic effect.</li>
<li>DropText: Activates the effect that the Text falls and then appears the new one from the top.</li>
<li>Slide: Activates the effect having a Panel that appears from the right.</li>
<li>Fall: Activates the effect for UIElements to fall down and then you can fall 'up'.</li>
</ul>

With that you can have the following XAML:

```xaml

 <Grid x:Name="TitleBackground" a:Effects.BlendForeground="Assets/Backgrounds/masterbackground.jpg" Background="Black" />
 
 <TextBlock Text="id Software Games" x:Name="TitleLabel" a:Effects.DropText="id Software Games"/>
 
```

and so on.

<b>Notes</b>

I have seen that setting 
```
new PropertyMetadata(DependencyProperty.UnsetValue...)
```
does not fire the first time, that's why I keep the first slide on, to check if in a new SDK release is solved.

Apart, you have to be aware that when it enters in detail mode, the composition layer translates the items but there are still there, so you have to set the scrollviever collapsed to be able to touch the other parts.


<h2>Composition Manager</h2>

I have extracted the methods from the Composition Effects Editor Example to give everyone the chance to test it witouth struggling how to get it.

<b>Notes</b>

You cannot set the Children of the ContainerVisual of an Element directly you must use something like CreateContainerVisual.
I simplify the method ResizeImage from the example setting Stretch UniformToFill in the SpriteVisual.

<h2>Animation Composition Manager</h3>

This is cool, I have setup several example that are linked to the attached properties in order to make easy to change animations depending your needs.

<h2>To Do</h2>
I would like to make an expansion when I tap an Image from the details and expand to the whole window.

Hope you find useful and fun, and remember Composition does not use Dependency Properties, that's is one of the many things that makes it bettar than Storyboards

<a hef="https://twitter.com/juanpaexpedite">@juanpaexpedite</a>



