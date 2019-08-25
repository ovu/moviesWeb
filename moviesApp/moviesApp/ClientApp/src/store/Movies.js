const requestMoviesType = 'REQUEST_MOVIES';
const receiveMoviesType = 'RECEIVE_MOVIES';
const initialState = { movies: [], isLoading: false };

export const actionCreators = {
  requestMovies: () => async (dispatch, getState) => {
    if (getState().moviesReducer.isLoading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: requestMoviesType });

    const url = 'api/Movies/list';
    const response = await fetch(url);
    const movies = await response.json();

    dispatch({ type: receiveMoviesType, movies });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestMoviesType) {
    return {
      ...state,
      isLoading: true
    };
  }

  if (action.type === receiveMoviesType) {
    return {
      ...state,
      movies: action.movies,
      isLoading: false
    };
  }

  return state;
};
