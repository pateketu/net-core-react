import * as React from 'react';
import IAnswerProps from './IAnswerProps';

export const AnswerInput: React.SFC<IAnswerProps> = (props) => {
    return <div>
                <div className="equation">
                    <div>{props.a}</div>
                    <div>+</div>
                    <div>{props.b}</div>
                    <div>=</div>
                    <div><input type="text"
                            ref={(input) => input && input.focus() }
                            onChange={(input) =>
                             props.onAnswer(
                                input.target.value.trim().length > 0 ?
                                    Number(input.target.value) :
                                    NaN)
                        }/></div>
                </div>
            </div>;
};
