import * as React from 'react';
import Timer from './Timer';
import {shallow, mount} from 'enzyme';
jest.useFakeTimers();
describe('Timer Component', () => {
    it('Lifecycle', () => {
        jest.useFakeTimers();
        let isExpired = false;
        const wrapper = shallow(<Timer seconds={1} onExpired={() => (isExpired = true)}>
                                    <div id="child">Child component</div>
                                </Timer>);

        expect(wrapper.find('#child')).toHaveLength(1);

        jest.advanceTimersByTime(1000);

        setImmediate(() => {
           expect(wrapper.type()).toBeNull();
           expect(isExpired).toBeTruthy();
        });
    });

});
