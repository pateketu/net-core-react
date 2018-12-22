import React from 'react';
import App from './App';
import {mount, shallow} from 'enzyme';
import * as gameService from './domain/gameService';
import { Promise, Deferred } from 'q';
import Game from './domain/game';
import toJson from 'enzyme-to-json';

describe('Game App', () => {
  it('Renders game', (done) => {
      const mockStart = jest.spyOn(gameService, 'start');
      const fakeGame = {...new Game(), a: 6, b: 6, timeFrameSeconds: 20, level: 'Beginner'};

      const promise = Promise((r) => r(fakeGame));
      mockStart.mockReturnValue(promise);

      const wrapper = mount(<App/>);

      promise.then(() => {
         setImmediate(() => {
          wrapper.update();
          expect(toJson(wrapper, {noKey: true})).toMatchSnapshot();
          done();
        });
      }, () => {/*Reject*/});
  });
});
