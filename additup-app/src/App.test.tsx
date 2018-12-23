import React from 'react';
import App from './App';
import {mount} from 'enzyme';
import * as gameService from './domain/gameService';
import { Promise } from 'q';
import Game from './domain/game';
import toJson from 'enzyme-to-json';
jest.useFakeTimers();
describe('Game App', () => {
  // tslint:disable-next-line:no-empty
  let gameStartPromise: Promise<Game> = Promise<Game>(() => {});
  let wrapper = null;
  beforeEach(() => {
      const mockStart = jest.spyOn(gameService, 'start');
      const fakeGame = {...new Game(), a: 6, b: 6, timeFrameSeconds: 1, level: 'Beginner'};

      gameStartPromise = Promise((r) => r(fakeGame));
      mockStart.mockReturnValue(gameStartPromise);
  });

  it('Renders game', async () => {

      wrapper = mount(<App/>);

      await gameStartPromise;
      wrapper.update();
      expect(toJson(wrapper, {noKey: true})).toMatchSnapshot();
  });

  describe('Re-start', () => {
      it('Shows re-start button when time expires', async () => {
          wrapper = mount(<App/>);

          await gameStartPromise;

          jest.advanceTimersByTime(1000);
          wrapper.update();

          expect(wrapper.find('button').text()).toBe('Re-Start');
      });

      it('Clicking on Re-start button, re-renders the game', async () => {
          wrapper = mount(<App/>);

          await gameStartPromise;

          jest.advanceTimersByTime(1000);
          wrapper.update();

          wrapper.find('button').simulate('click');

          await gameStartPromise;
          wrapper.update();

          expect(toJson(wrapper, {noKey: true})).toMatchSnapshot();
      });

  });
  describe('Answer', () => {
      it('Answering correctly, advances the game to next level', async () => {
          wrapper = mount(<App/>);

          await gameStartPromise;
          wrapper.update();

          wrapper.find('button').simulate('click');

      });
  });
});
