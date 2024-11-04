import { expect } from "chai";
import { describe, it } from "mocha";

import { API_BASE_URL, axios, EMPTY_GUID } from "../test.config";

type Book = {
    bookId?: string;
    title: string;
    subtitle?: string;
    publishedDate?: Date;
    createdTime?: Date;
    lastModifiedTime?: Date;
}

describe("/books", () => {
    let rootBook: Book;

    before(async () => {
        const res = await axios.post(`${API_BASE_URL}books`, {
            "title": "Le Petit Programmeur",
            "publishDate": "2024-11-03T00:00:00.00"
        });
        expect(res.status).to.equal(201);
        expect(res.data).to.be.an('object');
        rootBook = res.data;
    });

    it("should return 200 OK with a page of data on unparameterized GET", async () => {
        const res = await axios.get(`${API_BASE_URL}books`);
        expect(res.status).to.equal(200);
        expect(res.data).to.be.an('object');

        const books: Book[] = res.data.items as Book[];
        expect(books.length).to.be.greaterThan(0);
        expect(books).to.deep.include(rootBook);
    });

    it("should return 201 Created on POST", async () => {
        const res = await axios.post(`${API_BASE_URL}books`, {
            "title": "Le Petit Programmeur",
            "publishDate": "2024-11-03T00:00:00.00"
        });
        expect(res.status).to.equal(201);
        expect(res.data).to.be.an('object');

        const book: Book = res.data as Book;
        expect(book.bookId).to.not.be.undefined;
        expect(book.title).to.equal("Le Petit Programmeur");
        expect(book.createdTime).to.not.be.undefined;
        expect(book.lastModifiedTime).to.not.be.undefined;
    });

    it("should return 200 OK and the requested book", async () => {
        const res = await axios.get(`${API_BASE_URL}books/${rootBook.bookId}`);
        expect(res.status).to.equal(200);
        expect(res.data).to.be.an('object');

        const book: Book = res.data as Book;
        expect(book).to.be.deep.equal(rootBook);
    })

    it("should return 404 NOT FOUND for non-existing book", async () => {
        const res = await axios.get(`${API_BASE_URL}books/${EMPTY_GUID}`, {
            validateStatus: () => true,
        });
        expect(res.status).to.equal(404);
    })
});
