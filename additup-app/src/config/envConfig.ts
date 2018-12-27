const localhost = {
    api: {
        url: 'http://localhost:56000',
    },
};

const prod = {
    api: {
        url: 'http://localhost:56000',
    },
};

const config = process.env.REACT_APP_GAME_ENV === 'prod'
  ? prod
  : localhost;

export default config;
