import * as React from 'react';
import renderer from 'react-test-renderer';
import Answer from './Answer';
import {mount} from 'enzyme';

describe('Answer  Component', () => {
    it('Renders expected html mark-up', () => {
        // tslint:disable-next-line:no-empty
        const tree = renderer.create(<Answer a={1} b={2} onAnswer={() => {}}></Answer>).toJSON();
        expect(tree).toMatchSnapshot();
    });

    it('Renders an error message & disables button for an invalid answer entry', () => {
        const wrapper = mount(<Answer a={1} b={2}
            // tslint:disable-next-line:no-empty
            onAnswer={(answer: number ) => { }}></Answer>);

        wrapper.find('input').simulate('change', {target: {value: 'foo'}});

        expect(wrapper.find('.invalid')).toHaveLength(1);
        expect(wrapper.find('button').prop('disabled')).toBeTruthy();
    });

    it('Clicking on Answer button fires onAnswer with expected answer value', () => {
        let result: number = 0;
        const value: number = 20;

        const wrapper = mount(<Answer a={1} b={2}
            onAnswer={(answer: number ) => { result = answer; }}></Answer>);

        wrapper.find('input').simulate('change', {target: {value}});
        wrapper.find('button').simulate('click');

        expect(result).toBe(value);
    });

});
