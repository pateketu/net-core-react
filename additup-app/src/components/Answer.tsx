import * as React from 'react';
import {AnswerInput} from './AnswerInput';
import IAnswerProps from './IAnswerProps';

interface IAnswerState {
    answer: number;
}
export default class Answer extends React.PureComponent<IAnswerProps, IAnswerState> {
    constructor(props: IAnswerProps) {
        super(props);

    }
    public render() {
        return  <div>
            <AnswerInput a={this.props.a}
                         b={this.props.b}
                         onAnswer={(answer: number) => {
                             this.setState({answer});
                        }}
            ></AnswerInput>
            {this.state && isNaN(this.state.answer)
                && <div className="invalid">Please provide a valid number </div>}
            <button disabled={!this.state || isNaN(this.state.answer)}
                onClick={() => {this.props.onAnswer(this.state.answer);  }}>Asnwer</button>
        </div>;
    }

}
