import React from 'react';
import App from './App';
import {mount} from 'enzyme';
import * as gameService from './domain/gameService';
import { Promise, Deferred } from 'q';
import Game from './domain/game';
import toJson from 'enzyme-to-json';

// jest.useFakeTimers();
describe('Game App', () => {
  it('Shows game equation', (done) => {
      const mockStart = jest.spyOn(gameService, 'start');
      const promise = Promise((r) => r(new Game()));
      mockStart.mockReturnValue(promise);

      const wrapper = mount(<App/>);
      // jest.advanceTimersByTime(1000);
      promise.then(() => {
        wrapper.update();
        console.log(wrapper.html());
        // setTimeout(() => {
        //   // expect(toJson(wrapper, {noKey: true})).toMatchSnapshot();
        //   console.log(wrapper.debug());
        //   done();
        // }, 2000);
      });
  });
});
