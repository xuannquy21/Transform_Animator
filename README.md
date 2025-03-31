<h1> Transform Animation </h1>

This package has the basic features so you can control an Animation to run at the desired time interval including rewinding easily.

<h2>Step 1: Bake</h2>
- Add TransformAnimatorBaker to the GameObject that matches your Animation
- Add the Animation Clips you want (remember the order of the Animation Clips)
- Click on the 3 dots icon in the upper right corner of TransformAnimatorBaker, select Bake
- A TransformAnimator Component will be added to your GameObject along with the data in TranformAnimationRuntimes.

- The only thing you need to care about is the Length of each TranformAnimationRuntime because it is the time interval of the corresponding Animation Clip.

<h2>Step 2: Control</h2>
- Now, create a new script to be able to control the time interval of your animation.
![Transform Animation Script](https://github.com/user-attachments/assets/5e2d18e1-374f-4c50-8cbd-a0b8140447b8)
- You can remove TransformAnimatorBaker

- You can refer to AnimatorHandle.cs in Demo

Have a nice day!!!
