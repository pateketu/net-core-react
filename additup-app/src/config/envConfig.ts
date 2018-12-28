const localhost = {
    api: {
        url: 'http://localhost:56000',
    },
};

const prod = {
    api: {
        url: '',
    },
};

const config = process.env.REACT_APP_GAME_ENV === 'local'
  ? localhost
  : prod;

export default config;
