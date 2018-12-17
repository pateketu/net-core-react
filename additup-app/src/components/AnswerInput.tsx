import * as React from 'react';

interface IAnswerInputProps {
    a: number;
    b: number;
    onAnswerChanged: (answer: number) => void;
}

export const AnswerInput: React.SFC<IAnswerInputProps> = (props) => {
    return <div>
                <div className="equation">
                    <h4>{props.a}</h4>
                    <h4>+</h4>
                    <h4>{props.b}</h4>
                    <div><input type="text" onChange={(input) =>
                            props.onAnswerChanged(Number(input.target.value))
                        }/></div>
                </div>
            </div>;
};
