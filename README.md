# MauiReversi

These past two weeks were my best attempt at making the game Reversi, in .NET Maui. It did not go as well as I had hoped. While I did learn a lot, both about .NET Maui and my general habits and faults as a developer, my goals were ultimately not reached. Though I may continue to work on this in my free time, I can't devote any more class time to what has become a mess of code that needs a serious overhaul.

#### Why did I fail?

There are lots of reasons why this project did not go terribly well, but I would say the biggest problem is the development of a new habit for me. 

###### Revision after Revision

I noticed at many points throughout this project, that I was revising more past code than I was writing new code. Even though A function would work, I kept revising it more and more, to make it do its job better and faster. This ultimately led to my downfall, as this left me no time for bug testing or fixing in the end. 

###### Grids Are Not So Good

I spent a huge amount of time trying to figure out the best way to index a grid. In other words, to ask the grid for a thing at a location and then get it. This does exist in Maui by default, but it isn't a property of a Grid, so it took some time to find and figure out how to use it properly. Once I did eventually find it - it was the Grid.ElementAt() method if you are curious, it did not work how I would have liked. It accepts a single integer, where I wanted to specify a row and column. The Grid.GetColumn and Grid.GetRow methods come in handy for this, and from there, it's just a simple formula to get the actual index. That solution was discovered fairly early, but when, how, and where to use it are something I am still pondering. This was a big part of all of my revisions.

#### Code Review

Now I am going to review the code I wrote and decide whether it is great and clean or terrible and messy.

##### Generating the Grid - Good and Clean

Easily the best code I wrote in this project is the code for generating the grid. It was clean, specific, efficient, and dynamic. It also generates the buttons along with it. The syntax for this is pretty strange, especially for adding onClick methods to the generated buttons.

##### Handling Clicks - Good and Clean

The way I handled clicks is to first grab and calculate the index of the button and then pass that to the other function calls. The OnClick function acted as a sort of hub area, that would call many functions, some recursive, some not, that would eventually return to the OnClick function to run the next step or complete the process. After several revisions, it was in this function that I started splitting up lines of code by functionality into their own functions that were called one by one. Sometimes these functions only had a few lines of code in them, but because they were named appropriately and had their own functionality, it really helped me keep track of what was happening.

##### Finding the Grid boundaries - Horrible and Messy

So what I was doing was using 4 separate for-loops to find the boundaries and add their indexes to lists, which I would then respond to inside of the navigation functions in a somewhat poor manner. But using indexing for this is just bad because the way to check for an index in a grid (as I have said) is really not so great in Maui.

What needs to happen is either one of two things:

1. Make an invisible outside layer of the grid that can be granted properties, like color or text that can be identified without indexing. One problem with this is that it really messes with my current way of indexing the grid

2. Make the grid the normal size, and give the outside layer a property that can identify it as a boundary without making it invisible. An example would be giving the boundary buttons secret text that is the same color as their background color. This would not mess up my indexing but requires some other complicated trickery.

##### Navigating the Grid and processing moves - Horrible and Messy

Okay, last thing. I have yet to find a better way of navigating the grid (moving in 8 directions) but the one I have is not working for me. It's recursive by nature and quickly becomes messy and complicated, not to mention there is currently an infinite loop in my version that went undetected for a long time and is not yet identified.

I can honestly say that while I am kicking around many ideas, I do not have a conceptual solution for this problem. I do know that whatever I try next, will be using rows and columns for navigation instead of indexing, but that requires some work in other functions that I am still working out.
