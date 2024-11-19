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

    beforeAll(async () => {
        const res = await axios.post(`${API_BASE_URL}books`, {
            "title": "Le Petit Programmeur",
            "publishDate": "2024-11-03T00:00:00.00"
        });
        expect(res.status).toBe(201);
        expect(res.data).toEqual(expect.objectContaining({})); // TBD: match object contents
        rootBook = res.data;
    });

    it("should return 200 OK with a page of data on unparameterized GET", async () => {
        const res = await axios.get(`${API_BASE_URL}books`);
        expect(res.status).toBe(200);
        expect(res.data).toEqual(expect.objectContaining({})); // TBD: match object contents

        const books: Book[] = res.data.items as Book[];
        expect(books.length).toBeGreaterThan(0);
        expect(books).toContainEqual(rootBook);
    });

    it("should return 201 Created on POST", async () => {
        const res = await axios.post(`${API_BASE_URL}books`, {
            "title": "Le Petit Programmeur",
            "publishDate": "2024-11-03T00:00:00.00"
        });
        expect(res.status).toBe(201);
        expect(res.data).toEqual(expect.objectContaining({
            "title": "Le Petit Programmeur",
            "createdTime": expect.any(String),
            "lastModifiedTime": expect.any(String),
        })); // TBD: match object contents
    });

    it("should return 200 OK and the requested book", async () => {
        const res = await axios.get(`${API_BASE_URL}books/${rootBook.bookId}`);
        expect(res.status).toBe(200);
        expect(res.data).toBeDefined()
        expect(res.data).toEqual(rootBook);
    })

    it("should return 404 NOT FOUND for non-existing book", async () => {
        const res = await axios.get(`${API_BASE_URL}books/${EMPTY_GUID}`, {
            validateStatus: () => true,
        });
        expect(res.status).toBe(404);
    })
});
