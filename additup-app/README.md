
### React App

Boostrapped based on create-react-app for TypeScript

### High Level Design

There are four components in the app:
    1. AnswerInput.tsx -- Functional Component responsible for rendering the actual equation (a + b = input)
    2. Answer.tsx -- Pure Component responsible for capturing answer from AnswerInput and validating it
    3. Timer.tsx -- Higher Order component that wraps Answer component, Timer component will remove it's chidlren it Time has expired
    4. App.tsx -- Main statefull component responsible for fetching and posting data