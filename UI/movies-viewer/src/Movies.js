import React, { Component } from "react";
import { variables } from "./Variables.js";

export class Movies extends Component {

    constructor(props) {
        super(props);

        this.state = {
            movies: [],
            modalTitle: "",
            MovieName: "",
            MovieId: 0,
            MovieIdFilter: "",
            MovieNameFilter: "",
            MovieGenreFilter: "",
            MovieActorFilter: "",
            moviesWithoutFilter: []
        }
    }

    FilterFn() {
        var MovieIdFilter = this.state.MovieIdFilter;
        var MovieNameFilter = this.state.MovieNameFilter;
        var MovieGenreFilter = this.state.MovieGenreFilter;
        var MovieActorFilter = this.state.MovieActorFilter;

        var filteredData = this.state.moviesWithoutFilter.filter(
            function (el) {
                return el.MovieId.toString().toLowerCase().includes(
                    MovieIdFilter.toString().trim().toLowerCase()
                ) &&
                    el.Title.toString().toLowerCase().includes(
                        MovieNameFilter.toString().trim().toLowerCase()
                ) &&
                    el.Genre.toString().toLowerCase().includes(
                      MovieGenreFilter.toString().trim().toLowerCase()
                )
                &&
                el.Actors.toString().toLowerCase().includes(
                  MovieActorFilter.toString().trim().toLowerCase()
            )
            }
        );

        this.setState({movies:filteredData});
    }

    changeMovieIdFilter = (e) => {
        this.state.MovieIdFilter = e.target.value;
        this.FilterFn();
    }

    changeMovieNameFilter = (e) => {
        this.state.MovieNameFilter = e.target.value;
        this.FilterFn();
    }

    changeMovieGenreFilter = (e) => {
        this.state.MovieGenreFilter = e.target.value;
        this.FilterFn();
    }
    
    changeMovieActorFilter = (e) => {
        this.state.MovieActorFilter = e.target.value;
        this.FilterFn();
    }

    refreshList() {
        fetch(variables.API_URL + 'movie')
            .then(resp => resp.json())
            .then(data => {
                this.setState({ movies: data, moviesWithoutFilter: data });
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    changeMovieTitle = (e) => {
        this.setState({ MovieName: e.target.value });
    }

    addClick() {
        this.setState({
            modalTitle: "Add Movie",
            MovieId: 0,
            MovieName: ""
        });
    }

    editClick(mov) {
        this.setState({
            modalTitle: "Edit Movie",
            MovieId: mov.MovieId,
            MovieName: mov.MovieName
        });
    }

    render() {
        const {
            movies,
            modalTitle,
            MovieName,
            MovieId
        } = this.state;

        return (
            <div>
                <button type='button' className='btn btn-primary m-2 float-end'
                    data-bs-toggle="modal" data-bs-target="#exampleModal"
                    onClick={() => this.addClick()}>
                    Add Movie
                </button>
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>
                                <input className='form-control m-2'
                                    onChange={this.changeMovieIdFilter}
                                    placeholder="Filter By Id" />
                                Id
                            </th>
                            <th>
                                <input className='form-control m-2'
                                    onChange={this.changeMovieNameFilter}
                                    placeholder="Filter By Title" />
                                Title
                            </th>
                            <th>
                            <input className='form-control m-2'
                                    onChange={this.changeMovieGenreFilter}
                                    placeholder="Filter By Genre" />
                                Genre
                            </th>
                            <th>
                            <input className='form-control m-2'
                                    onChange={this.changeMovieActorFilter}
                                    placeholder="Filter By Actor" />
                                Actors
                            </th>
                            <th>
                                Options
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {movies.map(x =>
                            <tr key={x.MovieId}>
                                <td>{x.MovieId}</td>
                                <td>{x.Title}</td>
                                <td>{x.Genre}</td>
                                <td>{x.Actors}</td>
                                <td>
                                    <button type="button" className="btn btn-light mr-1" data-bs-toggle="modal" data-bs-target="#exampleModal"
                                        onClick={() => this.editClick(x)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>
                                    <button type="button" className="btn btn-light mr-1">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                                        </svg>
                                    </button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>

                <div className='modal fade' id="exampleModal" tabIndex="-1" aria-hidden="true">
                    <div className='modal-dialog modal-lg modal-dialog-centered'>
                        <div className='modal-content'>
                            <div className='modal-header'>
                                <h5 className='modal-title'>{modalTitle}</h5>
                                <button type='button' className='btn-close' data-bs-dismiss="modal" aria-label='Close'></button>

                                <div className='modal-body'>
                                    <div className='imput-group mb-3'>
                                        <span className='input-group-text'>Movie Title</span>
                                        <input type="text" className="form-control"
                                            value={MovieName}
                                            onChange={this.changeMovieTitle} />
                                    </div>

                                    {MovieId === 0 ?
                                        <button type='button' className='btn btn-primary float-start'>Create</button>
                                        : null}

                                    {MovieId !== 0 ?
                                        <button type='button' className='btn btn-primary float-start'>Update</button>
                                        : null}

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}