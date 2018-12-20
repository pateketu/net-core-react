import * as React from 'react';
import renderer from 'react-test-renderer';
import { AnswerInput} from './AnswerInput';
import {shallow} from 'enzyme';

describe('Answer Input Component', () => {
    it('Renders expected html mark-up', () => {
        // tslint:disable-next-line:no-empty
        const tree = renderer.create(<AnswerInput a={1} b={2} onAnswer={() => {}}></AnswerInput>).toJSON();
        expect(tree).toMatchSnapshot();
    });

    it('Fires onAnswer with expected answer value', () => {
        let result: number = 0;
        const value: number = 20;

        const wrapper = shallow(<AnswerInput a={1} b={2}
            onAnswer={(answer: number ) => { result = answer; }}></AnswerInput>);

        wrapper.find('input').simulate('change', {target: {value}});

        expect(result).toBe(value);
    });

    it('Fires onAnswer with NaN value', () => {
        let result: number = 0;

        const wrapper = shallow(<AnswerInput a={1} b={2}
            onAnswer={(answer: number ) => { result = answer; }}></AnswerInput>);

        wrapper.find('input').simulate('change', {target: {value: 'foo'}});

        expect(result).toBe(NaN);
    });
});
