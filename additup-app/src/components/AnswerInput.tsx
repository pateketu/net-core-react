import * as React from 'react';
import IAnswerProps from './IAnswerProps';

export const AnswerInput: React.SFC<IAnswerProps> = (props) => {
    return <div>
                <div className="equation">
                    <h4>{props.a}</h4>
                    <h4>+</h4>
                    <h4>{props.b}</h4>
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
