# beat-synchronizer-unity #

## Beat Synchronizer for Unity ##

This set of classes allows the user to synchronize events, actions, and behaviors of a Unity object to 
the beats of an audio clip. It also includes a pattern counter that takes a sequence of 
unique beats to drive the synchronization behavior.

Most any tempo value is supported as well as a wide variety of beat options, provided that the 
tempo/beats aren't so fast that Unity's `Update()` function won't be able to handle synchronization 
accurately.

### Included scripts ###

	BeatSynchronizer
		SynchronizerData.cs
		BeatSynchronizer.cs
		BeatCounter.cs
		PatternCounter.cs
		BeatObserver.cs
	Editor
		BeatCounterEditor.cs
		PatternCounterEditor.cs

### Sample usage ###

See `ExampleScene` in the Unity project.

---

### License ###

The MIT License (MIT)

Copyright (c) 2014 Christian Floisand

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
	
