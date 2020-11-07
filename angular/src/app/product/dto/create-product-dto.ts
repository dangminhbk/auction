export class CreateProductDto {
        name: string;
        price: number;
        description: string;
        coverImageId: number;
        imageIds: number[];
        categoryIds: number[];
        sellerId: number;
        brandId: number;
}
