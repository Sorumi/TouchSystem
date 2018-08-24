# TouchSystem
🖐🏻 Gestures and touch system for Unity.

You can easy control touch inputs and gestures with TouchSystem. TouchSystem provides variety built in recognizers and assumes that only one gesture can be recognized at a time. 

### Feature
#### Customizable gesture recognizers
If you want to make your own gesture recognizer, all you have to do is subclass **AbstractRecognizer** and implement some methods.

#### Control continuous recognize
**Continuous recognition** means that another gesture can be triggered when one finger leaves the screen and the others remain.

### Included Gesture Recognizers
- Tap Recognizer
- Pan Recognizer (one or more fingers)
- Pinch Recognizer (two fingers)

### TODO
- Add gesture target: using collision or rect
- Add more gestures
