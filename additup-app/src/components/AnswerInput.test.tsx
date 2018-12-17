import * as React from 'react';
import renderer from 'react-test-renderer';
import { AnswerInput} from './AnswerInput';
describe('Answer Input Component', () => {
    it('Renders expected html mark-up', () => {
        // tslint:disable-next-line:no-empty
        const tree = renderer.create(<AnswerInput a={1} b={2} onAnswerChanged={() => {}}></AnswerInput>).toJSON();
        expect(tree).toMatchSnapshot();
    });
});
