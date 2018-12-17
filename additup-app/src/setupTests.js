import {configure} from 'enzyme';
import Adapter from 'enzyme-adapter-react-16';
// https://github.com/facebook/create-react-app/pull/5698 haven't got the bug fix yet hence using .js
configure({ adapter: new Adapter() });
