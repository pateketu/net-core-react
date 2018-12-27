import React from 'react';
import App from './App';
import {mount} from 'enzyme';
import * as gameService from './domain/gameService';
import { Promise } from 'q';
import Game from './domain/game';
import toJson from 'enzyme-to-json';
import { GameResult } from './domain/gameResult';
jest.useFakeTimers();
describe('Game App', () => {
  // tslint:disable-next-line:no-empty
  let gameStartPromise: Promise<Game> = Promise<Game>(() => {});
  let wrapper: any = null;
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
      // tslint:disable-next-line:no-empty
      let answerPromise: Promise<GameResult> = Promise<GameResult>(() => {});

      beforeEach(async () => {
        wrapper = mount(<App/>);

        await gameStartPromise;
        wrapper.update();
      });
      const customBeforeEach = async (mockGame: Game | undefined = undefined,
                                      returnFinished: boolean= false,
                                      returnInCorrectAnswer: boolean= false) => {

            const mockAnswer = jest.spyOn(gameService, 'answer');
            const fakeResult: GameResult = {...new GameResult(),
                                   allLevelsFinished: returnFinished,
                                   game: mockGame,
                                   inCorrectAnswer: returnInCorrectAnswer };

            answerPromise = Promise((r) => r(fakeResult));
            mockAnswer.mockReturnValue(answerPromise);
      };

      const simulateAnswer = async (answer: string) => {
        // Answer by simulating an input and click button
        wrapper.find('input').simulate('change', {target: {value: answer}});
        wrapper.find('button').simulate('click');
        await answerPromise;
        wrapper.update();
      };

      it('Answering correctly, advances the game to next level', async () => {
            customBeforeEach({...new Game(), a: 1, b: 1, timeFrameSeconds: 1, level: 'Talented'});

            await simulateAnswer('12');

            expect(wrapper.text()).toContain('Your current level is Talented');

      });

      it('Answering in-correctly, shows Incorrect message and re-start button', async () => {
            customBeforeEach(undefined, false, true);

            await simulateAnswer('12');

            expect(wrapper.text()).toContain('Incorrect Answer');
            expect(wrapper.find('button').text()).toBe('Re-Start');
      });

      it('Answering last question correctly, shows game finished message and re-start button', async () => {
        customBeforeEach(undefined, true);

        await simulateAnswer('12');

        expect(wrapper.text()).toContain('Game Finished');
        expect(wrapper.find('button').text()).toBe('Re-Start');
  });
  });

});
