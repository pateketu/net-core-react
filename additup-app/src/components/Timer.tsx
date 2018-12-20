import * as React from 'react';

interface ITimerProps {
    seconds: number;
    onExpired: () => void;
}

interface ITimerState {
    remainingSeconds: number;
}
export default class Timer extends React.PureComponent<ITimerProps, ITimerState> {

    private timer: any = null;
    constructor(props: ITimerProps) {
        super(props);
        this.state = {remainingSeconds: props.seconds};
    }
    public componentDidMount() {
        this.timer = setInterval(() => {
                const timeRemaining = this.state.remainingSeconds - 1;
                if (timeRemaining <= 0) {
                    this.props.onExpired();
                } else {
                    this.setState({remainingSeconds: timeRemaining});
                }

        }, 1000);
    }

    public componentWillUnmount() {
        if (this.timer) {
            clearInterval(this.timer);
        }
    }
    public render() {

        if (this.state.remainingSeconds > 0 ) {
            return <React.Fragment>
                    <div>{this.state.remainingSeconds} Seconds Remaining</div>
                    {this.props.children}
            </React.Fragment>;
        } else {
            return null;
        }
    }

}
